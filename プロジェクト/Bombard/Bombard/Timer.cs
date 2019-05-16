using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bombard
{
    class Timer
    {
        private float currentTimer;
        private float limitTimer;
        private float v;

        public Timer()
        {
            limitTimer = 60.0f;
        }
        public Timer(float second)
        {
            limitTimer = 60.0f * second;
        }

        public Timer(float second, float v) : this(second)
        {
            this.v = v;
        }

        public void Initialize()
        {
            currentTimer = limitTimer;
        }

        public void Update()
        {
            currentTimer -= 1.0f;
            if (currentTimer < 1.0f)
            {
                currentTimer = 0.0f;
            }
        }
        public float Now()
        {
            return currentTimer;
        }

        public bool IsTime()
        {
            return currentTimer <= 0.0f;
        }
        public void Change(float limitTime)
        {
            this.limitTimer = limitTime;
            Initialize();
        }
        public float Rate()
        {
            return currentTimer / limitTimer;
        }
    }
}
