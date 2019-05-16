using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bombard
{
    class GameOver:IScene
    {
        private bool endFlag;
        private bool isPressKey;
        private int timer;
        private Sound sound;
      


        public GameOver(Sound sound)
        {
            this.sound = sound;
        }
        public void Initialize()
        {
           
            timer = 0;
            endFlag = false;        // シーン継続に設定
            isPressKey = true;      // 「押した」に設定
            //sound.PlayBGM("bgm_ending");
            //sound.PlaySE("se_over");
        }
        public void Update()
        {
          
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                // 前回が押してなければ
                if (isPressKey == false)
                {
                    endFlag = true;       // シーン終了に設定
                    isPressKey = true;     // 「押した」に設定
                }
            }
            else
            {
                isPressKey = false;     // 「押してない」に設定
            }
    
            timer++;
        }
        public void Draw(Renderer renderer)
        {
            if (timer % 60 < 30)
            {
                renderer.DrawTexture("overtext", new Vector2(290, 160), 0.6f);
            }
            else
            {
                renderer.DrawTexture("overtext", new Vector2(290, 160));
            }
 

        

        }

        public bool IsEnd()
        {
            return endFlag;
        }
        public Scene Next()
        {
            return Scene.Title;
        }
    }
}
