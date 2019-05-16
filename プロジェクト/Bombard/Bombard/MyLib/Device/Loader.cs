using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLib.Device
{
    /// <summary>
    /// 読み込み抽象クラス
    /// </summary>
    public abstract class Loader
    {
        protected string[,] resources;//リソースアセット名群
        protected int counter;//カウンタ
        protected int maxMun;//リソース最大数
        protected bool endFlag;//終了フラグ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="resources"></param>
        public Loader(string[,] resources)
        {
            this.resources = resources;
        }
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            counter = 0;
            endFlag = false;
            maxMun = 0;
            if (resources != null)
            {
                //配列から、登録する個数を取得
                maxMun = resources.GetLength(0);
            }
        }
        /// <summary>
        /// 現在登録している番号の取得
        /// </summary>
        /// <returns></returns>
        public int CurrentCount()
        {
            return counter;
        }
        /// <summary>
        /// 登録最大数を取得
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return maxMun;
        }
        /// <summary>
        /// 終了フラグ
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            return endFlag;
        }
        public abstract void Update();
    }
}
