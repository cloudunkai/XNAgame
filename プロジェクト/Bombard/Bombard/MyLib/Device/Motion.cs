using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyLib.Device;
using MyLib.Utility;

namespace MyLib.Device
{
    public class Motion
    {
        private Range range;
        private Timer timer;
        private int motionNumber;

        //表示位置を番号で管理
        //Dictionaryを使えば登録順番を気にしなくてよい
        private Dictionary<int, Rectangle> rectangles = new Dictionary<int, Rectangle>();

        public Motion()
        {
            //何もしない
            Initialize(new Range(0, 0), new Timer());
        }
        public Motion(Range range, Timer timer)
        {
            Initialize(range, timer);
        }
        public void Initialize(Range range, Timer timer)
        {
            this.range = range;
            this.timer = timer;
            //モーション番号の最初は範囲の最初に設定
            motionNumber = range.First();
        }

        public void Add(int index, Rectangle rect)
        {
            if (rectangles.ContainsKey(index))
            {
                return;
            }
            rectangles.Add(index, rect);
        }

        private void MotionUpdate()
        {
            motionNumber += 1;//モーション番号をインクリメント
            //選択外なら最初に戻す
            if (range.isOutOfRange(motionNumber))
            {
                motionNumber = range.First();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (range.isOutOfRange())
            {
                return;
            }
            timer.Update();
            if (timer.IsTime())
            {
                timer.Initialize();
                MotionUpdate();
            }
        }
        public Rectangle DrawingRange()
        {
            return rectangles[motionNumber];
        }
    }
}
