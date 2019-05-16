using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bombard
{
    class Player
    {

        private Vector2 position;
        private Vector2 size;//プレーヤーサイズ
        private Vector2 halfSize;//ステージ判定用
        private Direction dir;//方向
        private int timer;//時間
        private ActionMode mode;
        private Vector2 speed;
        private Sound sound;
        private bool isShoot;//必殺技中
        private bool canShoot;//必殺技使用可能
        private int shootCount;//間隔
        private int mp;//ｍｐ
        public Player(Sound sound)
        {
            position = new Vector2(0, 0);
            size = new Vector2(128, 128);
            halfSize = new Vector2(64, 64);
            speed = new Vector2();
            this.sound = sound;

            mp = 5;
        }
        public Direction GetDirection()
        {
            return dir;
        }
        public ActionMode GetMode()
        {
            // モードを返す
            return mode;
        }
        public void Initialize()
        {
            mp = 5;

            timer = 0;
            dir = Direction.Right;

            position.X = 200;
            position.Y = Screen.BaseY - halfSize.Y;
            mode = ActionMode.Stand;
            isShoot = false;
            canShoot = true;
            shootCount = 0;
        }
        public int Getmp()
        {
            return mp;
        }
        public void Addmp()
        {
            mp = mp + 1;
        }
        public void Move()
        {
            // 攻撃中は移動できない
            //if (mode == ActionMode.Attack)
            //{
            //    return;
            //}
            //必殺技
            if (mode == ActionMode.Shootting)
            {
                return;
            }
         //左右移動
            if (Keyboard.GetState().IsKeyDown(Keys.Right) ||
    GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
            {
                dir = Direction.Right;
                position.X += 6;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) ||
    GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
            {
                dir = Direction.Left;
                position.X -= 6;
            }
        }


        private void InScreen()
        {
            //ステージのあたり判定
            Vector2 min = halfSize;
            Vector2 max = new Vector2(Screen.Width, Screen.Height) - halfSize;
            position = Vector2.Clamp(position, min, max);
        }
        private void AnimationUpdate()
        {
            // 加算
            timer++;
            // モードにより対応したアニメーションを行う
            switch (mode)
            {
                // 移動（4パターンで戻す）
                case ActionMode.Move:
                    if (timer >= 15 * 4)
                    {
                        timer = 0;
                    }
                    break;

                // 立ち（2パターンで戻す）
                case ActionMode.Stand:
                    if (timer >= 20 * 2)
                    {
                        timer = 0;
                    }
                    break;
                // 攻撃（4パターンで終了）
                case ActionMode.Attack:
                    if (timer >= 5 * 4)
                    {
                        // 立ちへ変更
                        mode = ActionMode.Jump;
                        timer = 0;
                    }
                    break;
                case ActionMode.JumpAttack:
                    if (timer >= 30)
                    {
                        // ジャンプへ変更
                        mode = ActionMode.Jump;
                        timer = 0;
                    }
                    break;
                case ActionMode.Shootting:
                    if (timer >= 18 * 3)
                    {
                        mode = ActionMode.Jump
                            ;
                        timer = 0;
                    }

                    break;
            }
        }
        private void ChangeMode()
        {
            // 左右を押していれば
            if (Keyboard.GetState().IsKeyDown(Keys.Right) ||
                Keyboard.GetState().IsKeyDown(Keys.Left)
                )
            {
                // 立ちモードなら、移動モードへ切替え
                if (mode == ActionMode.Stand)
                {
                    mode = ActionMode.Move;   // 移動モードに切替え
                    timer = 0;                  // タイマーの初期化
                }
            }
            // 押してなければ
            else
            {
                // 移動モードなら、立ちモードへ切り替え
                if (mode == ActionMode.Move)
                {
                    mode = ActionMode.Stand;                           // 立ちモードに切替え
                    timer = 0;
                }
            }
        }
        public void JumpStart()
        {

            // ジャンプ中なら開始が出来ない
            // （ここの改造で２段ジャンプに出来る）
            if (mode == ActionMode.Jump || mode == ActionMode.JumpAttack)
            {
                return;

            }
            // ＺキーかＡボタンでジャンプ開始
            if (Keyboard.GetState().IsKeyDown(Keys.Z) ||
                GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                
                speed.Y = -17;                // 最初のスピードを設定
                mode = ActionMode.Jump;     // モードをジャンプに設定
            }
        }

        public void JumpUpdate()
        {
            // ジャンプ中のみ処理をする
            if (mode == ActionMode.Jump || mode == ActionMode.JumpAttack)
            {
                // ジャンプ実施
                position.Y += speed.Y;           // Ｙ座標にスピードを加える
                speed.Y += 0.5f;	          // 重力に引かれスピードが下方向に増える

                // 地面についたら
                if (position.Y >= Screen.BaseY - halfSize.Y)
                {

                    position.Y = Screen.BaseY - halfSize.Y;     // 地面にめりこまないようにＹを補正
                    mode = ActionMode.Stand;                                     // モードを立ちに設定
                    timer = 0;                                  // タイマーを初期化
                }

            }
        }
        public void JumpAttackStart()
        {
            if (mode == ActionMode.Jump)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.X) ||
                GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                {
                    
                    mode = ActionMode.JumpAttack;
                    timer = 0;
                }
            }
        }
        public void AttackStart()
        {
            // 移動か立ちなら攻撃開始
            if (mode == ActionMode.Stand || mode == ActionMode.Move || mode == ActionMode.Shootting)
            {
                // ＸキーかＢボタンキーで攻撃開始
                if (Keyboard.GetState().IsKeyDown(Keys.X) ||
                    GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                {
                    mode = ActionMode.Attack;
                    // モードを攻撃に設定
                    sound.PlaySE("se_attack");        // 攻撃ＳＥプレイ
                    timer = 0;                               // タイマーのクリア
                }
            }
        }
        public bool IsShoot()
        {
            return isShoot;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public void Update()
        {
            AnimationUpdate();
            Move();
            InScreen();
            ChangeMode();
            JumpStart();      // ジャンプ開始
            JumpUpdate();      // ジャンプ更新
            AttackStart();  // 攻撃開始
            JumpAttackStart();  // ジャンプ攻撃開始
            shoot();
        }
        public void shoot()
        {


            if (mp >= 5)
            {
                if (mode == ActionMode.Stand || mode == ActionMode.Jump || mode == ActionMode.JumpAttack || mode == ActionMode.Move&&canShoot == false)
                {
                    shootCount += 1;
                    if (shootCount > 10)
                    {
                        canShoot = true;
                        shootCount = 0;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.C) && canShoot)
                {
                    sound.PlaySE("se_damage");
                    mode = ActionMode.Shootting;
                    isShoot = true;
                    canShoot = false;
                    mp = mp - 5;
                    timer = 0;
                }
                else
                {
                    if (isShoot)
                        isShoot = false;
                }
            }
            else
            {
                isShoot = false;
            }
        }
        public void Draw(Renderer renderer)
        {

            renderer.DrawTexture("block2", new Vector2(80, 610), new Vector2(1.0f * Getmp(), 1.0f));
            // 切出し用（安全の為に0で初期化）
            int sx = 0, sy = 0;
            switch (mode)
            {
                // 移動
                case ActionMode.Move:
                    // ０→１→２→１の順番でアニメーション
                    sx = timer / 15;
                    if (sx == 3)
                    {
                        sx = 1;
                    }
                    sy = 1;
                    break;
                // 立ち
                case ActionMode.Stand:
                    // ２パターンでアニメーション
                    sx = timer / 20;
                    sy = 0;
                    break;
                case ActionMode.Jump:
                    sx = 2;
                    sy = 0;
                    break;
                case ActionMode.Attack:
                    // 順番にアニメーション
                    sx = timer / 5;
                    sy = 2;
                    break;
                case ActionMode.JumpAttack:
                    sx = 3;
                    sy = 0;
                    break;
                case ActionMode.Shootting:
                    sx = timer / 10;
                    sy = 3;
                    break;

                // それ以外（ありえないはずだが、安全の為に記述）
                default:
                    break;


            }
            sx *= (int)size.X;
            sy *= (int)size.Y;
            Rectangle rect = new Rectangle(sx, sy, (int)size.X, (int)size.Y);
            if (dir == Direction.Right)
            {
                renderer.DrawTexture("player", position - halfSize, rect);
            }
            // 左向きならば
            else
            {
                // 切出し位置の変更
                rect.X = 512 - rect.X - (int)size.X;

                // 表示（名前と座標と切り出す図形を指定）
                renderer.DrawTexture("player_left", position - halfSize, rect);
            }
        }

    }
}
