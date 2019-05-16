using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard
{
    class Explosion
    {
        private Vector2 position;
        private bool isDead;
        private int cnt;
        private Motion motion;
    

        public Explosion(Vector2 position)
        {
            this.position = position;
            isDead = false;
            cnt = 0;
            //必殺技を使用とき　playerのアニメーション表示　１０フレーム
            motion = new Motion();
            for(int i=0;i<=9;i++)
            {
                motion.AddRectangle(i, new Rectangle(128 * i, 0, 128, 128));
            }
            motion.Initialize(0, 10, 3);

        }
        public void Initialize()
        {

        }
        public void Update()
        {
            //表示時間
            cnt += 1;
            if(cnt>20)
            {
                isDead = true;
            }
            motion.Update();
        }
        public void Draw(Renderer renderer)
        {
            //イメージ
            renderer.DrawTexture("explosion", position - new Vector2(64, 64), motion.CurrentRectangle());
        }
        public bool IsDead()
        {
            return isDead;
        }
    }
}
