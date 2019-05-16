using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Bombard
{
    class TimerUI
    {
        private Timer timer;
        public TimerUI(Timer timer)
        {
            this.timer = timer;
        }
        public void Draw(Renderer renderer)
        {
            //タイム表示
            renderer.DrawTexture("timer", new Vector2(600, 10));
            string gameTime = (timer.Now() / 60).ToString();
            int digit = 5;
           
            if (gameTime.Length > digit)
            {
                renderer.DrawNumber("number", new Vector2(800, 10), gameTime, digit);
            }
            else
            {
                renderer.DrawNumber("number", new Vector2(800, 10), gameTime, gameTime.Length);
            }
        }
    }
}
