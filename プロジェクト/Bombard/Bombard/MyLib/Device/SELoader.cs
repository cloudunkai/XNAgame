using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLib.Device
{
    public class SELoader : Loader
    {
        private Sound sound;
        public SELoader(Sound sound, string[,] resources) :
            base(resources)
        {
            this.sound = sound;
            Initialize();
        }
        public override void Update()
        {
            endFlag = true;
            if (counter < maxMun)
            {
                sound.LoadSE(resources[counter, 0], resources[counter, 1]);
                counter += 1;
                endFlag = false;
            }
        }

    }
}
