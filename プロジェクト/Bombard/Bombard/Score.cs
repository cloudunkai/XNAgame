using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Bombard
{
    class Score
    {
        private int gameScore;
        public Score()
        {
            
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            // 変数の初期化
            gameScore = 0;              // ゲームスコア
        }


        /// <summary>
        /// 表示
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            // スコア文字の表示（名前と座標を指定）
            renderer.DrawTexture("score", new Vector2(20, 4));


            // スコアの数字表示（名前、座標、数値、桁数を指定）
            renderer.DrawNumber("number", new Vector2(200, 13), gameScore,4);

            renderer.DrawTexture("block1", new Vector2(70, 600));

        }

        /// <summary>
        /// 加算
        /// </summary>
        public void Add()
        {
            // スコアの加算
            gameScore += 10;
        }

        /// <summary>
        /// ゲームスコアの獲得
        /// </summary>
        /// <returns></returns>
        public int GetGameScore()
        {
            // ゲームスコアを返す
            return gameScore;
        }


    }
}
