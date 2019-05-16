using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLib.Utility
{
    public class Range
    {
        private int first;
        private int end;

        public Range(int first, int end)
        {
            this.first = first;
            this.end = end;
        }

        public int First()
        {
            return first;
        }
        public int End()
        {
            return end;
        }
        public bool IsWithin(int num)
        {
            if (num < first)
            {
                return false;
            }
            if (num > end)
            {
                return false;
            }
            return true;
        }
        public bool isOutOfRange()
        {
            return first >= end;
        }

        public bool isOutOfRange(int num)
        {
            return !IsWithin(num);
        }
    }
}
