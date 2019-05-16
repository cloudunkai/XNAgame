
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// XNA特有の機能
using Microsoft.Xna.Framework;          // 基本機能

namespace Bombard
{
    /// <summary>
    /// フェードイン
    /// </summary>
    class Fade
    {
        // メンバー変数の宣言
        private bool mode;      // モード
        private float timer;      // タイマー

        /// <summary>
        /// コンストラクタ（生成時に自動的に呼び出される）
        /// </summary>
        public Fade()
        {

        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            mode = true;        // フェード中に設定
            timer = 1;        // タイマーの初期化
        }


        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            // フェードイン中は
            if (mode == true)
            {
                timer -= 0.01f;    // 値を減らす

                // 最小値で
                if (timer <= 0)
                {
                    mode = false;   // フェードイン終了
                }
            }
        }



        /// <summary>
        /// 表示
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            // フェード中で
            if (mode == true)
            {
                // タイマーの明るさで表示
                renderer.DrawTexture("fade", new Vector2(0, 0), timer);
            }
        }

    }
}
