using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Bombard
{
    class GamePlay : IScene
    {
        private CharacterManager characterManager;
        private Collision collision;
        private bool endFlag;
        private Stage stage;
        private Player player;
        private Timer timer;
        private TimerUI timerUI;
        private Sound sound;
        private Score score;


        public GamePlay(Sound sound)
        {
            score = new Score();
            stage = new Stage();
            player = new Player(sound);
            this.sound = sound;
        
            characterManager = new CharacterManager(sound, player, score);
            collision = new Collision(sound, player, characterManager, score);     // 衝突判定
        }
        public void Initialize()
        {
            timer = new Timer(60.0f);
            timer.Initialize();
            timerUI = new TimerUI(timer);
            score.Initialize();
            player.Initialize();
            endFlag = false;
            characterManager.Initialize();
        }

        public void Update()
        {
            timer.Update();
            player.Update();
            characterManager.Update();
            collision.Update();
            //シーン変換の試し
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                endFlag = true;
            }
            //時限死亡
            if (timer.IsTime())
            {
                endFlag = true;
            }
            if (collision.IsEnd() == true)
            {
                endFlag = true;  // シーン終了に設定
                sound.StopBGM();       // ＢＧＭ停止
            }
            // 落下でゲーム終了
          
        }
        public void Draw(Renderer renderer)
        {
            //画像表示
            stage.Draw(renderer);
            player.Draw(renderer);
            characterManager.Draw(renderer);
            score.Draw(renderer);
            timerUI.Draw(renderer);
        }
        public bool IsEnd()
        {
            return endFlag;
        }
        public Scene Next()
        {
            return Scene.GameOver;
        }
    }
}
