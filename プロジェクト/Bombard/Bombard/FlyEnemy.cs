using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Bombard
{
    class FlyEnemy
    {
        private int timer;
        private Vector2 position;   // 位置
        private Vector2 size;       // サイズ
        private Vector2 halfSize;
        private Vector2 speed;
        private Direction dir;
        private bool deadFlag;
        private ActionMode mode;
        private float radius;

        public FlyEnemy()
        {
            speed = new Vector2();
            Random rand = new Random();
            timer = 0;
            position = new Vector2(0, 0);
            size = new Vector2(128, 128);
            halfSize = new Vector2(64, 64);
            position.X = halfSize.X;
            deadFlag = false;
            radius = 64;
            mode = ActionMode.Move;
            // Ｙ座標の設定

            position.Y = Screen.BaseY - halfSize.Y;


            speed.X = rand.Next(2, 7);
            speed.Y = 0;

            if (rand.Next(2) == 0)
            {
                position.X = -halfSize.X;              // Ｘ座標
                position.Y = 200;
                dir = Direction.Right;                  // 右向きにする
            }
            // 右から発生
            else
            {
                position.Y = 200;
                position.X = Screen.Width + halfSize.X; // Ｘ座標
                dir = Direction.Left;   // 左向きにする
            }



        }

        public void Update()
        {
            Move();     // 移動
            AnimationUpdate();
            InScreen();
        }
        private void InScreen()
        {
            // 画面の左に出たら
            if (position.X < -halfSize.X)
            {
                deadFlag = true;      // 削除するように設定
            }

            // 画面の右に出たら
            if (position.X > Screen.Width + halfSize.X)
            {
                deadFlag = true;      // 削除するように設定
            }
        }
        public bool IsDead()
        {
            return deadFlag;
        }


        private void MoveRight()
        {
            position.X += speed.X;    // 右へ移動
        }
        private void MoveLeft()
        {
            position.X -= speed.X;    // 右へ移動
        }
        private void Move()
        {
            if (dir == Direction.Right)
            {
                MoveRight();    // 右へ移動
            }
            // 左向きなら
            else
            {
                MoveLeft();    // 左へ移動
            }
            if (mode == ActionMode.Damage)
            {
                deadFlag = true;
                //// ジャンプ実施
                //position.Y += speed.Y;      // Ｙ座標にスピードを加える
                //speed.Y += 0.5f;	        // 重力に引かれスピードが下方向に増える
            }

        }
        private void AnimationUpdate()
        {
            // 加算
            timer++;

            // 移動（4パターンで戻す）
            if (timer >= 15 * 4)
            {
                timer = 0;
            }

        }
        public float GetRadius()
        {
            return radius;
        }
        public void Hit()
        {


            mode = ActionMode.Damage;   // ダメージ中にする

            timer = 0;                  // タイマーの初期化

            //speed.X *= -1;              // 移動量の反転

            //speed.Y = -16;              // ジャンプの初速

        }

        public ActionMode GetMode()
        {
            // モードを返す
            return mode;
        }

        public Vector2 GetPosition()
        {
            return position;    // 座標を返す
        }





        /// <summary>
        /// 表示
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            int sx, sy;


            // 移動
            // ０→１→２→１の順番でアニメーション
            sx = timer / 15;
            if (sx == 3)
            {
                sx = 1;
            }
            sy = 1;
            sx *= (int)size.X;
            sy *= (int)size.Y;



            Rectangle rect = new Rectangle(sx, sy, (int)size.X, (int)size.Y);

            if (dir == Direction.Right)
            {
                // 表示（名前と座標と切り出す図形を指定）
                renderer.DrawTexture("Fly", position - halfSize, rect);
            }
            // 左向きならば 
            else
            {
                // 切出し位置の変更
                rect.X = 512 - rect.X - (int)size.X;

                // 表示（名前と座標と切り出す図形を指定）
                renderer.DrawTexture("Fly_left", position - halfSize, rect);
            }





        }



    }
}
