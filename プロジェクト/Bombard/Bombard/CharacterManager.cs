using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard
{
    class CharacterManager
    {

        private List<Explosion> explosionList;
        private List<Enemy> enemyList;
        private List<Bullet> bulletList;
        private List<FlyEnemy> flyenemyList;
        private Score score;
        private Sound sound;        // サウンド
        private Player player;      // プレイヤー
        private Random rand;
        // キャラクターを複数入れるリスト型のコレクション

        /// <summary>
        /// コンストラクタ（生成時に自動的に呼び出される）
        /// </summary>
        /// <param name="sound"></param>
        /// <param name="player"></param>
        public CharacterManager(Sound sound, Player player,Score score)
        {
            rand = new Random();
            // 情報の登録
            this.score = score;
            this.sound = sound;     // サウンド
            this.player = player;     // プレイヤー
            enemyList = new List<Enemy>();
            bulletList = new List<Bullet>();
            flyenemyList = new List<FlyEnemy>();
            explosionList = new List<Explosion>();

        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            enemyList.Clear();

            flyenemyList.Clear();
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            foreach (var f in flyenemyList)
            {
                f.Update();
            }
            foreach (var e in enemyList)
            {
                e.Update();
            }
            foreach (var b in bulletList)
            {
                b.Update();
            }
            foreach(var ex in explosionList )
            {
                ex.Update();
            }

            EnemyBorn();
            FlyEnemyBorn();
            bulletPosition();
            Explosion();
            enemyList.RemoveAll(e => e.IsDead() == true);
            flyenemyList.RemoveAll(e => e.IsDead() == true);
            bulletList.RemoveAll(b => b.IsDead() == true);
            explosionList.RemoveAll(b => b.IsDead() == true);

        }

        void EnemyBorn()
        {

            if (rand.Next(150) == 0)
            {
                enemyList.Add(new Enemy());


            }
        }
        void FlyEnemyBorn()
        {

            if (rand.Next(150) == 0)
            {
                flyenemyList.Add(new FlyEnemy());


            }
        }
        public List<Enemy> GetEnemyList()
        {
            return enemyList;    // 敵コレクションの情報を返す
        }
        public List<FlyEnemy> GetFlyEnemyList()
        {
            return flyenemyList;    // 敵コレクションの情報を返す
        }

        public List<Bullet> GetBulletList()
        {
            return bulletList;    // 敵コレクションの情報を返す
        }
        public void bulletPosition()
        {
            if (player.IsShoot())
            {


                for (float i = 0; i < 360; i += 0.1f)
                {
                    float speed = 15;
                    float angle = (float)MathHelper.ToRadians(i);
                    float vx = speed * (float)Math.Cos(angle);
                    float vy = speed * (float)Math.Sin(angle);
                    bulletList.Add(new Bullet(player.GetPosition() + new Vector2(0, 0), new Vector2(vx, vy)));

                }

            }

        }
        public void Explosion()
        {
            foreach(Enemy enemy in enemyList)
            {
                if(enemy.IsDead())
                {
                    explosionList.Add(new Explosion(enemy.GetPosition()));
                }
            }
            foreach(FlyEnemy flyenemy in flyenemyList)
            {
                if(flyenemy.IsDead())
                {
                    explosionList.Add(new Explosion(flyenemy.GetPosition()));
                }
            }
        }
        /// <summary>
        /// 表示
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            // 敵コレクションの表示
            foreach (var e in enemyList)
            {
                e.Draw(renderer);     // 敵の表示
            }

            foreach (var b in bulletList)
            {
                b.Draw(renderer);
            }
            foreach (var f in flyenemyList)
            {
                f.Draw(renderer);     // 敵の表示
            }
            foreach(var ex in explosionList)
            {
                ex.Draw(renderer);
            }

        }
    }

}

