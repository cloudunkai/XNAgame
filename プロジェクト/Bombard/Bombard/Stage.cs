using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard
{
    class Stage
    {
        public  Stage()
        {

        }
        public  void Draw(Renderer renderer)
        {
            renderer.DrawTexture("stage", new Vector2(0, 0));
        }
    }
}
