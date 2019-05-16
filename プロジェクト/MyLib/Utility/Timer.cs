using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLib.Utility
{
    public class Timer
    {
        private float currentTime;
        private float limitTime;
        private int timer;

        /// <summary>
        /// デフォルトコンストラクタ
        /// （１秒に設定）
        /// </summary>
        public Timer()
        {
            //1秒,60FPS
            limitTime = 60;
            timer = 0;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="second"制限時間秒></param>
        public Timer(float second)
        {
            limitTime = 60 * second;
            timer = 0;
            //limitTime = second;
            Initialize();
        }
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            currentTime = limitTime;
            timer = 0;
        }
        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            currentTime -= 1.0f;
            if (currentTime < 0.0f)
            {
                currentTime = 0.0f;
            }
            timer += 1;
        }
        public bool IsTime()
        {
            return timer > limitTime;
        }
        public float Now()
        {
            return currentTime;
        }
        public void Add(float currentTime)
        {
            this.currentTime = this.currentTime + (currentTime * 60);
        }
        public bool IsTimecurrent()
        {
            return currentTime == 0;
        }
        public void Change(float limitTime)
        {
            this.limitTime = limitTime;
            Initialize();
        }
        public float Rate()
        {
            return Math.Min(1.0f, timer / limitTime);
        }
    }
}
