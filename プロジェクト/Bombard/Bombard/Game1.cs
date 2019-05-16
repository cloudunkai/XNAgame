using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Bombard
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Sound sound;
        private Renderer renderer;
        private SceneManager sceneManager;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = Screen.Width;
            graphics.PreferredBackBufferHeight = Screen.Height;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Window.Title = "Bombard";

            sound = new Sound(Content);
            renderer = new Renderer(Content, GraphicsDevice);

            sceneManager = new SceneManager();
            sceneManager.Add(Scene.Load, new Load(sound));

       
            sceneManager.Add(Scene.Title, new Title(sound));        // タイトル
            sceneManager.Add(Scene.GamePlay, new GamePlay(sound));  // ゲームプレイ
            sceneManager.Add(Scene.GameOver, new GameOver(sound));      // ゲームオーバー
            sceneManager.Change(Scene.Load);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //renderer.LoadTexture("titlebg");
            //renderer.LoadTexture("overtext");
            //renderer.LoadTexture("titletext");
            //renderer.LoadTexture("number");
            //renderer.LoadTexture("stage");
            //renderer.LoadTexture("bridge");
            //renderer.LoadTexture("starttext");
           
            //renderer.LoadTexture("player");
            //renderer.LoadTexture("player_left");

            renderer.LoadTexture("load");
            // TODO: use this.Content to load your game content here
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            renderer.Unload();
            sound.Unload();

        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            sceneManager.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.Black);
            renderer.Begin();

            //renderer.DrawTexture("titlebg", new Vector2(0, 0));
            //renderer.DrawTexture("titletext", new Vector2(220, 0), 0.6f);
            sceneManager.Draw(renderer);
           
   
            renderer.End();


            base.Draw(gameTime);
        }
    }
}
