using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Bombard
{
    class Collision
    {
        private Sound sound;        // サウンド
        private Player player;      // プレイヤー
        private CharacterManager characterManager;//キャラクター管理
        private bool endFlag;
        private Score score;
        private Random rand;//敵ランダムで出現


        public Collision(Sound sound, Player player, CharacterManager characterManager, Score score)
        {
            this.sound = sound;
            this.player = player;
            this.score = score;
            this.characterManager = characterManager;
            rand = new Random();
        }
        public void Update()
        {
            CollisionPlayerEnemy();
            CollisionPlayerAttack();
            CollisionEnemyBullet();
        }


        private void CollisionEnemyBullet()
        {
            Vector2 enemyPosition = new Vector2(0, 0);
            Vector2 bulletPosition = new Vector2(0, 0);
            Vector2 FlyenemyPosition = new Vector2(0, 0);
            
            List<Bullet> bulletList = characterManager.GetBulletList();
            List<Enemy> enemyList = characterManager.GetEnemyList();
            List<FlyEnemy> flyenemyList = characterManager.GetFlyEnemyList();
            //スキルと敵の判定
            foreach (var e in enemyList)//普通エネミー
            {
                foreach (var b in bulletList)
                {
                    if (e.GetMode() != ActionMode.Damage)
                    {
                        float distance = Vector2.Distance(enemyPosition, bulletPosition);
                        if (distance < 50)
                        {
                            e.Hit();
                            score.Add();//加点
                        }
                    }
                }

            }
            foreach (var f in flyenemyList)//飛ぶエネミー
            {
                foreach (var b in bulletList)
                {
                    if (f.GetMode() != ActionMode.Damage)
                    {
                        float distance = Vector2.Distance(FlyenemyPosition, bulletPosition);
                        if (distance < 50)
                        {
                            f.Hit();
                            score.Add();
                        }
                    }
                }

            }
        }

        private void CollisionPlayerEnemy()
        {

            Vector2 playerPosition = new Vector2(0, 0);
            Vector2 enemyPosition = new Vector2(0, 0);
            Vector2 FlyenemyPosition = new Vector2(0, 0);
            
            playerPosition = player.GetPosition();//player位置
            endFlag = false;
            List<Enemy> enemyList = characterManager.GetEnemyList();
            List<FlyEnemy> flyenemyList = characterManager.GetFlyEnemyList();
            foreach (var e in enemyList)
            {
                //ダメージを受けたら　死亡
                if (e.GetMode() != ActionMode.Damage)
                {
                    enemyPosition = e.GetPosition();
                    float distance = Vector2.Distance(playerPosition, enemyPosition);
                    if (distance < 50)
                    {
                        endFlag = true;
                    }
                }

            }
            foreach (var f in flyenemyList)
            {
                // ダメージ中は衝突しない
                // （ダメージ中以外で衝突判定処理を行う）
                if (f.GetMode() != ActionMode.Damage)
                {

                    // 敵の座標の獲得
                    FlyenemyPosition = f.GetPosition();

                    // ２つの円の中心の距離を求める
                    float distance = Vector2.Distance(playerPosition, FlyenemyPosition);

                    // 一定範囲で衝突
                    if (distance < 50)
                    {
                        endFlag = true;
                        // シーン終了に設定
                    }
                }

            }
        }
     

        public bool IsEnd()
        {
            // 終了か？を返す
            return endFlag;
        }
        private void CollisionPlayerAttack()
        {
            // 攻撃中のみ実施
            ActionMode mode = player.GetMode();
            if (mode == ActionMode.Attack || mode == ActionMode.JumpAttack)
            {

            }
            else
            {
                return;
            }

            // 座標を入れる変数の宣言と生成
            Vector2 attackPosition = new Vector2(0, 0);
            Vector2 enemyPosition = new Vector2(0, 0);
            Vector2 FlyenemyPosition = new Vector2(0, 0);
            // プレイヤー座標の獲得
            // まず攻撃座標にプレイヤー座標を入れる
            attackPosition = player.GetPosition();

            // 方向で攻撃座標の補正
            if (player.GetDirection() == Direction.Right)
            {
                attackPosition.X += 50; // 右向き
            }
            else
            {
                attackPosition.X -= 50; // 左向き　
            }

            // 敵コレクションの獲得
            List<Enemy> enemyList = characterManager.GetEnemyList();
            List<FlyEnemy> flyenemyList = characterManager.GetFlyEnemyList();
            // 敵コレクションでループ
            foreach (var e in enemyList)
            {
                // ダメージ中は衝突しない
                // （ダメージ中以外で衝突判定処理を行う）
                if (e.GetMode() != ActionMode.Damage)
                {

                    // 敵の座標の獲得
                    enemyPosition = e.GetPosition();

                    // 距離を求める
                    float distance = Vector2.Distance(attackPosition, enemyPosition);

                    // 一定範囲で衝突
                    if (distance < 50)
                    {
                        
                        player.Addmp();
                        e.Hit();    // 衝突した処理
                        //sound.PlaySE("se_damage");   // ＳＥのプレイ
                        score.Add();
                    }
                }
            }
            foreach (var f in flyenemyList)
            {
                // ダメージ中は衝突しない
                // （ダメージ中以外で衝突判定処理を行う）
                if (f.GetMode() != ActionMode.Damage)
                {
                   

                    // 敵の座標の獲得
                    FlyenemyPosition = f.GetPosition();

                    // 距離を求める
                    float distance = Vector2.Distance(attackPosition, FlyenemyPosition);

                    // 一定範囲で衝突
                    if (distance < 50)
                    {
                        
                        player.Addmp();
                        f.Hit();    // 衝突した処理
                        sound.PlaySE("se_power");  // ＳＥのプレイ
                        score.Add();
                    }
                }
            }

        }


    }
}
