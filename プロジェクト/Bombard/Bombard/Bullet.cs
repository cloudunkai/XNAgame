using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard
{
    class Bullet
    {
        private Vector2 position;//位置
        private bool deadFlag;  //消える
        private Vector2 velocity;//スピード
        public Bullet(Vector2 position,Vector2 velocity)
        {
            this.velocity = velocity;    
            this.position = position;
            deadFlag = false;
        }
        public void Initialize()
        {
        }
        public void Update()
        {
            position = position + velocity;
            if (position.X <0)
            {
                deadFlag = true;
            }
            if (position.X > Screen.Width)
            {
                deadFlag = true;
            }
            if(position.Y>Screen.Height)
            {
                deadFlag = true;
            }
            if (position.Y <0)
            {
                deadFlag = true;
            }
        }
     
        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("shot", position );
        }
        public bool IsDead()
        {
            return deadFlag;
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public void Hit()
        {
            deadFlag = true;
        }
    }
}
