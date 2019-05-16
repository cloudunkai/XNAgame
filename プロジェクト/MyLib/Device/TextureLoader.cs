using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLib.Device
{
    /// <summary>
    /// テクスチャ読み込み
    /// </summary>
    public class TextureLoader : Loader
    {
        private Renderer renderer;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>
        /// <param name="resouces">アセット名群</param>
        public TextureLoader(Renderer renderer, string[,] resouces)
            : base(resouces)//親クラスで初期化
        {
            this.renderer = renderer;
            Initialize();//Loaderクラスの初期化処理を呼び出す
        }
        public override void Update()
        {
            //終了フラグを有効にして
            endFlag = true;
            //カウンタが最大に達していないか
            if (counter < maxMun)
            {
                //画像の読み込み
                renderer.LoadTexture(resources[counter, 0],
                    resources[counter, 1]);
                counter += 1;
                //読み込むものがあったので終了フラグを継続に設定
                endFlag = false;
            }
        }
    }
}
