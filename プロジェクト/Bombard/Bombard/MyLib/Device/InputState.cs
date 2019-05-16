using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;//Vector2用
using Microsoft.Xna.Framework.Input;//Keyboardクラス利用のため

namespace MyLib.Device
{
    public class InputState
    {
        private Vector2 velocity = Vector2.Zero;//移動量

        //キー
        private KeyboardState currentKey;
        private KeyboardState previousKey;
        private GamePadState currentPad;
        private GamePadState prebiosPad;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InputState()
        {
        }

        /// <summary>
        /// 移動量の取得
        /// </summary>
        /// <returns></returns>
        public Vector2 Velocity()
        {
            return velocity;
        }

        /// <summary>
        /// 移動量の更新
        /// </summary>
        /// <param name="KeyState">キーボードの状態</param>
        private void UpdateVelocity()
        {
            velocity = Vector2.Zero;//マイループ，ゼロで初期化

            if (CheckDownKey(Keys.Right, Buttons.DPadRight))
            //右
            {
                velocity.X += 1.0f;
            }
            if (CheckDownKey(Keys.Left, Buttons.DPadLeft))
           　//左
            {
                velocity.X -= 1.0f;
            }
            if (CheckDownKey(Keys.Up, Buttons.DPadUp))
            //上
            {
                velocity.Y -= 1.0f;
            }
            if (CheckDownKey(Keys.Down, Buttons.DPadDown))
            //下
            {
                velocity.Y += 1.0f;
            }

            //正規化
            if (velocity.Length() != 0.0f)
            {
                velocity.Normalize();//正規化メソッド
            }

        }
        /// <summary>
        /// キー情報の更新
        /// </summary>
        /// <param name="keyState"></param>
        private void UpdateKey(KeyboardState keyState)
        {
            //現在登録されているキーを１フレーム前のキーに
            previousKey = currentKey;
            //現在のキーを最新のキーに
            currentKey = keyState;
        }
        public void UpdatePad(GamePadState buttonState)
        {
            prebiosPad = currentPad;
            currentPad = buttonState;
        }
        /// <summary>
        /// キーが押されているか
        /// </summary>
        /// <param name="key">調べたいキー</param>
        /// <returns></returns>
        public bool IsKeyDown(Keys key)
        {
            //現在チェックしたいキーが押されたか
            bool current = currentKey.IsKeyDown(key);
            //1フレーム前に押されていたか
            bool previous = previousKey.IsKeyDown(key);
            //現在押されていて１フレーム前に押されていなければtrue
            return current && !previous;
        }
        public bool IsKeyDown(Buttons button)
        {
            bool current = currentPad.IsButtonDown(button);
            bool prebios = prebiosPad.IsButtonDown(button);
            return current && !prebios;
        }
        /// <summary>
        /// キー入力のトリガー判定
        /// </summary>
        /// <param name="key"></param>
        /// <returns>押されていたら</returns>
        public bool GetKeyTrigger(Keys key)
        {
            return IsKeyDown(key);
        }
        public bool GetKeyTrigger(Buttons button)
        {
            return IsKeyDown(button);
        }
        public bool GetKeyState(Buttons button)
        {
            return currentPad.IsButtonDown(button);
        }
        public bool GetKeyState(Keys key)
        {
            return currentKey.IsKeyDown(key);
        }
        public bool CheckDownKey(Keys key, Buttons button)
        {
            if (currentKey.IsKeyDown(key))
            {
                return true;
            }
            if (!GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                return false;
            }
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(button))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            var keyState = Keyboard.GetState();
            //キーの更新
            UpdateKey(keyState);
            var padState = GamePad.GetState(PlayerIndex.One);
            //ゲームパッドの更新
            UpdatePad(padState);
            //移動量の更新
            UpdateVelocity();
        }

    }
}
