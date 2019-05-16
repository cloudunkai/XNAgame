using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bombard
{
    class Title : IScene
    {

        private bool endFlag;
        private bool isPressKey;
        private int timer;
        private Sound sound;

        public Title(Sound sound)
        {
            this.sound = sound;
        }
        public void Initialize()
        {
            timer = 0;
            endFlag = false;        // シーン継続に設定
            isPressKey = true;      // 「押した」に設定
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
                    sound.StopBGM();
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
            
            renderer.DrawTexture("titlebg",new Vector2(0,0));
            renderer.DrawTexture("titletext", new Vector2(120, 150));
            if(timer%60<30)
            {
                renderer.DrawTexture("starttext", new Vector2(380, 450));
            }
            //半透明
            else
            {
                renderer.DrawTexture("starttext", new Vector2(380, 450), 0.3f);
            }
            
        }
        public bool IsEnd()
        {
            return endFlag;
        }

        public Scene Next()
        {
            return Scene.GamePlay;
        }
        public void Shutdown()
        {
        }
    }
}

