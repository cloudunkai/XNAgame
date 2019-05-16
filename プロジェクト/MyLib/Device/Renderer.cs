using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;//Vector2
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;//Assert用

namespace MyLib.Device
{
    public class Renderer
    {
        private ContentManager contentManager;//コンテンツ管理者
        private GraphicsDevice graphicsDevice;//グラフィック機器
        private SpriteBatch spriteBatch;//スプライト一括

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public Renderer(ContentManager content, GraphicsDevice graphics)
        {
            contentManager = content;
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }
        /// <summary>
        /// 画像の読み込み
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="filepath">画像までのパス</param>
        public void LoadTexture(string name, string filepath = "./")
        {
            //ガード節
            //Dictionaryへの2重登録回避
            if (textures.ContainsKey(name))
            {
#if DEBUG //DEBUGモード時のみ有効
                System.Console.WriteLine("この" + name + "はkeyで、すでに登録しています");

#endif
                //処理終了
                return;
            }
            //画像の読み込みとDicionaryへの登録
            textures.Add(name, contentManager.Load<Texture2D>(filepath + name));
        }
        /// <summary>
        /// 画像の登録
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="textuer">画像</param>
        public void LoadTexture(string name, Texture2D texture)
        {
            if (textures.ContainsKey(name))
            {
#if DEBUG
                System.Console.WriteLine("この" + name + "はKeyですでに登録されています");
#endif
                return;
            }
            textures.Add(name, texture);
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
        public void DrawNumber(string name, Vector2 position, int number, int digit, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(name),
                "1:アセット名をチェックしてください\n\r" +
                "2:LoadContentメソッドで画像を読み込んでるかをチェックしてください\n\r" +
                "Contentプロジェクトに画像が登録されてるかをチェックしてください");

            //1文字の大きさが幅３２、高さ６４として処理をかく
            //マイナスの数は０とする
            number = Math.Max(number, 0);

            foreach (var n in number.ToString().PadLeft(digit, '0'))//10進数1桁
            {
                spriteBatch.Draw(
                    textures[name],
                    position,
                    new Rectangle((n - '0') * 32, 0, 32, 64),
                    Color.White * alpha);
                position.X = position.X + 32;//横幅32ドット
            }
        }
        public void Unload()
        {
            //Dictionaryの登録情報のクリア
            textures.Clear();
        }

        public void Begin()
        {
            spriteBatch.Begin();
        }

        public void End()
        {
            spriteBatch.End();
        }

        public void DrawTexture(string name, Vector2 position, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(name),
                "アセット名が間違えていませんか?￥n" +
                "大文字小文字間違えていませんか？￥n" +
                "LoadTextureメソッドで読み込んでますか？￥n" +
                "プログラムを確認してください");

            spriteBatch.Draw(textures[name], position, Color.White * alpha);
        }
        public void DrawTexture(string name, Vector2 position, Rectangle rect, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違っていませんか？\n" +
                "LoadTextureメソッドで読み込んでいますか？\n" +
                "プログラムを確認してください\n");
            spriteBatch.Draw(textures[name], position, rect, Color.White * alpha);
        }
        public void DrawTexture(string name, Vector2 position, Rectangle rect, Vector2 scale, SpriteEffects sprite, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n",
                "大文字小文字間違っていませんか？\n",
                "LoadTextureメソッドで読み込んでいますか?\n",
                "プログラムを確認してください");

            spriteBatch.Draw(
                textures[name],         //画像
                position,               //位置
                rect,                   //切り取り範囲
                Color.White * alpha,    //透過
                0.0f,                   //回転
                Vector2.Zero,           //回転軸の位置
                scale,                  //拡大縮小
                sprite,                 //表示反転効果
                0.0f                    //スプライト表示深度
                );
        }
        /// <summary>
        /// 数字の描画(整数のみ版)
        /// </summary>
        /// <param name="アセット名"></param>
        /// <param name="位置"></param>
        /// <param name="表示したい数字"></param>
        /// <param name="透明度"></param>
        public void DrawNumber(string name, Vector2 position, int number, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(name),
                "アセット名が間違えていませんか?￥n" +
                "大文字小文字間違えていませんか？￥n" +
                "LoadTextureメソッドで読み込んでますか？￥n" +
                "プログラムを確認してください");

            //マイナスは０
            if (number < 0)
            {
                number = 0;
            }

            foreach (var n in number.ToString())
            {
                spriteBatch.Draw(textures[name], position, new Rectangle((n - '0') * 32, 0, 32, 64), Color.White * alpha);
                position.X += 32;//1桁分右にずらす
            }
        }

        public void DrawNumber(string name, Vector2 position, string number, int digit, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(name),
                "アセット名が間違えていませんか?￥n" +
                "大文字小文字間違えていませんか？￥n" +
                "LoadTextureメソッドで読み込んでますか？￥n" +
                "プログラムを確認してください");

            //桁数をループして１の位を表示
            for (int i = 0; i < digit; i++)
            {
                if (number[i] == '.')
                {
                    //幅をかけて座標を求め１文字を絵から取り出す
                    spriteBatch.Draw(textures[name], position, new Rectangle(10 * 32, 0, 32, 64), Color.White * alpha);
                }
                else
                {
                    //1文字分の数値を数値文字で取得
                    char n = number[i];

                    //幅をかけて座標を求め１文字を絵から取り出す
                    spriteBatch.Draw(textures[name], position, new Rectangle((n - '0') * 32, 0, 32, 64), Color.White * alpha);
                }
                position.X += 32;
            }
        }
    }
}
