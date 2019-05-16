using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Bombard
{
    class SceneManager
    {
        
        private IScene currentScene = null;
        private Fade fade;
        private Dictionary<Scene, IScene> scenes = new Dictionary<Scene, IScene>();
        public SceneManager()
        {
            fade = new Fade();
        }
        public void Add(Scene name, IScene scene)
        {
            scenes[name] = scene;
        }
        public void Change(Scene name)
        {
           
            currentScene = scenes[name];
            currentScene.Initialize();
            fade.Initialize();
        }
        public void Update()
        {
           
            currentScene.Update();
            if (currentScene.IsEnd())
            {
                Scene next = currentScene.Next();
                Change(next);
            }
            fade.Update();

        }
        public void Draw(Renderer renderer)
        {
            if (currentScene == scenes[Scene.Title] || currentScene == scenes[Scene.GamePlay])
            {
                fade.Draw(renderer);            // フェードインクラス
            }
            if (currentScene == scenes[Scene.GameOver])
            {
                scenes[Scene.GamePlay].Draw(renderer);
            }
           
            currentScene.Draw(renderer);
        }

    }
}
