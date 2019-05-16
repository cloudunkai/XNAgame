using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;//Vector2
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;//Assert用

namespace Bombard
{
     class Renderer
    {
        private ContentManager contentManager;//コンテンツ管理者
        private GraphicsDevice graphicsDevice;//グラフィック機器
        private SpriteBatch spriteBatch;//スプライト一括

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public Renderer(ContentManager content, GraphicsDevice graphics)
        {
            contentManager = content;
            contentManager.RootDirectory = "Content";
          
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphics);
        }
        /// <summary>
        /// 画像の読み込み
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="filepath">画像までのパス</param>
        public void LoadTexture(string name)
        {
            textures[name] = contentManager.Load<Texture2D>(name);
        }
        public void DrawTexture(string name, Vector2 position, float alpha = 1.0f)
        {


            spriteBatch.Draw(textures[name], position, Color.White * alpha);
        }
        public void DrawTexture(string name, Vector2 position, Rectangle rect, float alpha = 1.0f)
        {
            //スプライトバッチの描画機能を呼び出し、描画
            //第１引数：描画するtexture2Dを設定
            //（ディクショナリtexturesに登録されている、画像をアセット名を使い呼び出す）
            //第２引数：描画位置
            //第３引数：切り出す四角形を指定
            //第４引数：色を指定
            spriteBatch.Draw(textures[name], position, rect, Color.White * alpha);
        }

        public void DrawNumber(string name, Vector2 position, int number, int digit)
        {
            // この処理は１文字の大きさが幅32、高さ64とする

            // マイナスの数は０とする
            if (number < 0)
            {
                number = 0;
            }

            // 桁数だけ右へ表示座標を移動する
            position.X += (digit - 1) * 32;

            // 桁数ループして、１の位を表示
            for (int i = 0; i < digit; i++)
            {
                // 10で割る余りで、表示する数値を出す。
                int n = number % 10;

                // 幅を掛けて座標を求め、１文字を絵から切り出して表示
                spriteBatch.Draw(textures[name], position,
                                 new Rectangle(n * 32, 0, 32, 64), Color.White);

                // 数値を10で割ることで次の桁へ移動する。
                number /= 10;

                // 表示座標のＸ座標を左へ移動する（Ｘ座標から横幅の32を引く）
                position.X -= 32;

            }
        }
        public void DrawTexture(string name, Vector2 position, Vector2 scale, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n",
                "大文字小文字間違っていませんか？\n",
                "LoadTextureメソッドで読み込んでいますか?\n",
                "プログラムを確認してください");

            spriteBatch.Draw(
                textures[name],         //画像
                position,               //位置
                null,                   //切り取り範囲
                Color.White * alpha,    //透過
                0.0f,                   //回転
                Vector2.Zero,           //回転軸の位置
                scale,                  //拡大縮小
                SpriteEffects.None,     //表示反転効果
                0.0f                    //スプライト表示深度
                );
        }
        public void DrawNumber(string name,
          Vector2 position, string number, int dight, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(name),
               "アセット名が間違えていませんか?\n" +
               "大文字小文字間違ってませんか?\n" +
               "LoadTextureで読み込んでますか?\n" +
               "プログラムを確認してください");

            for (int i = 0; i < dight; i++)
            {
                if (number[i] == '.')
                {
                    spriteBatch.Draw(textures[name],
                        position, new Rectangle(10 * 32, 0, 32, 64), Color.White * alpha);
                }
                else
                {
                    char n = number[i];
                    spriteBatch.Draw(
                        textures[name], position,
                        new Rectangle((n - '0') * 32, 0, 32, 64), Color.White * alpha);

                }

                position.X += 32;
            }
        }

        public void Unload()
        {
            //Dictionaryの登録情報のクリア
            textures.Clear();
            contentManager.Unload();
        }

        public void Begin()
        {
            spriteBatch.Begin();
        }

        public void End()
        {
            spriteBatch.End();
        }

   
      
    

       
    }
}
