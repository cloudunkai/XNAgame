using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MyLib.Device
{
    public class GameDevice
    {
        private Renderer renderer;
        private InputState input;
        private Sound sound;
        private Random rand;
        private Vector2 displayModify;//ディスプレイ位置修正
        public GameDevice(ContentManager contentManager, GraphicsDevice graphics)
        {
            renderer = new Renderer(contentManager, graphics);
            input = new InputState();
            sound = new Sound(contentManager);
            rand = new Random();
        }

        public void Initizlize()
        { }

        public void LoadContent()
        {
            renderer.LoadTexture("number", "./Texture/");
            renderer.LoadTexture("load", "./Texture/");
            renderer.LoadTexture("title", "./Texture/");
        }
        public void UpDate(GameTime gameTime)
        {
            //デバイスで絶対に更新が必要なもの
            input.Update();
        }
        public void UnloadContent()
        {
            sound.Unload();
            renderer.Unload();
        }
        /// <summary>
        /// 描画オブジェクトの取得
        /// </summary>
        /// <returns></returns>
        public Renderer GetRenderer()
        {
            return renderer;
        }
        /// <summary>
        /// 入力オブジェクトの取得
        /// </summary>
        /// <returns></returns>
        public InputState GetInputState()
        {
            return input;
        }
        /// <summary>
        /// サウンドオブジェクトの取得
        /// </summary>
        /// <returns></returns>
        public Sound GetSound()
        {
            return sound;
        }
        /// <summary>
        /// 乱数オブジェクトの取得
        /// </summary>
        /// <returns></returns>
        public Random GetRandom()
        {
            return rand;
        }
        public void SetDisplayModify(Vector2 position)
        {
            this.displayModify = position;
        }
        public Vector2 GetDisplayModify()
        {
            return displayModify;
        }

    }
}
