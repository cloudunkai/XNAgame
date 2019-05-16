using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bombard
{
    class Load : IScene
    {
        private bool endFlag; 
        int timer;          
        private Sound sound;
        /// <summary>
        /// </summary>
        public Load(Sound sound)
        {
            this.sound = sound;     
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            endFlag = false;   // シーン継続に設定
            timer = 0;          // タイマーの初期化
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {

        }
        public void Draw(Renderer renderer)
        {
            // タイトルの表示（名前と座標を指定）
            renderer.DrawTexture("load", new Vector2(20, 20));

            // １ループ表示を行った後でロード処理を行う
            // 本来は表示に処理を書くのは好ましくないが、
            // rendererを使用するので、ここに書く
            timer++;
            if (timer > 2)
            {
                endFlag = true;     // シーン終了に設定
                renderer.LoadTexture("titlebg");
                renderer.LoadTexture("overtext");
                renderer.LoadTexture("titletext");
                renderer.LoadTexture("number");
                renderer.LoadTexture("stage");
               
                renderer.LoadTexture("starttext");
                renderer.LoadTexture("load");
                
                renderer.LoadTexture("shot");
                renderer.LoadTexture("player");
                renderer.LoadTexture("player_left");
                renderer.LoadTexture("enemy");
                renderer.LoadTexture("enemy_left");
              
                
                renderer.LoadTexture("fade");
                renderer.LoadTexture("timer");
                renderer.LoadTexture("block1");
                renderer.LoadTexture("block2");
                renderer.LoadTexture("explosion");
                renderer.LoadTexture("score");
                renderer.LoadTexture("Fly");
                renderer.LoadTexture("Fly_left");

                sound.LoadBGM("bgm_title");      // タイトル
                sound.LoadBGM("bgm_play");       // ゲームプレイ
                sound.LoadBGM("bgm_ending");    // エンディング

                // SEの読み込み（wav形式）
                sound.LoadSE("se_over", false);       // ゲームオーバー
                sound.LoadSE("se_start", false);      // ゲームスタート
                sound.LoadSE("se_jump", false);      // ジャンプ
                sound.LoadSE("se_power", false);
                sound.LoadSE("se_attack", false);    // 攻撃
                sound.LoadSE("se_damage", false);    // ダメージ



            }
        }
        public bool IsEnd()
        {
            // 終了か？を返す
            return endFlag;
        }
        public Scene Next()
        {
            // 次のシーンを返す（タイトル）
            return Scene.Title;
        }


    }
}
