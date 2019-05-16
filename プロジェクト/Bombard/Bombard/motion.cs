using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard
{
    class Motion
    {
        private int minIndex;

        private int maxIndex;

        private int interval;

        private int counter;

        private int currentIndex;

        private Dictionary<int, Rectangle> rectangles = new Dictionary<int, Rectangle>();

        public  Motion()
        {
            Initialize(0, 0, 0);
        }

        public void Initialize(int minIndex, int maxIndex, int interval)
        {
            this.minIndex = minIndex;
            this.maxIndex = maxIndex;
            this.interval = interval;
            counter = interval;
            currentIndex = minIndex;
        }

        public void AddRectangle(int index, Rectangle rectangle)
        {
            rectangles[index] = rectangle;
        }

        public void Update()
        {
            if (minIndex >= maxIndex)
            { return; }
            counter--;
            if (counter <= 0)
            {
                counter = interval;
                currentIndex++;
                if (currentIndex >= maxIndex)
                {
                    currentIndex = minIndex;
                }
            }
        }
        public Rectangle CurrentRectangle()
        {
            return rectangles[currentIndex];
        }
    }
}
