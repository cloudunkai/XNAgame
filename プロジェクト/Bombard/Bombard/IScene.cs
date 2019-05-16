using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Bombard
{
    interface IScene
    {
        void Initialize();
        void Update();
        void Draw(Renderer renderer);
        bool IsEnd();
        Scene Next();
    }
}
