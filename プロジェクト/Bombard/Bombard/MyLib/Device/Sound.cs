using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;//MP3データ
using System.Diagnostics;//Aseert

namespace MyLib.Device
{
    public class Sound
    {
        private ContentManager contentManager;

        private Dictionary<string, Song> bgms;//MP3管理用
        private Dictionary<string, SoundEffect> soundEffcts;//WAV管理用
        private Dictionary<string, SoundEffectInstance> seInstances;//WAVインスタンスの再生リスト
        private List<SoundEffectInstance> sePlayList;//WAVインスタンスの再生リスト

        private string currentBGM;//現在再生中のアセット名

        public Sound(ContentManager content)
        {
            contentManager = content;
            MediaPlayer.IsRepeating = true;

            bgms = new Dictionary<string, Song>();
            soundEffcts = new Dictionary<string, SoundEffect>();
            seInstances = new Dictionary<string, SoundEffectInstance>();

            sePlayList = new List<SoundEffectInstance>();

            currentBGM = null;
        }

        private string ErrorMessage(string name)
        {
            return "再生する音データのアセット名(" + name + ")がありません\n" +
                "アセット名の確認、Dictionaryに登録されているか確認してください\n";
        }

        #region BGM関連処理

        public void LoadBGM(string name, string filepath = "./")
        {
            //すでに登録されているか？
            if (bgms.ContainsKey(name))
            {
                return;
            }
            bgms.Add(name, contentManager.Load<Song>(filepath + name));
        }

        public bool IsStoppedBGM()
        {
            return (MediaPlayer.State == MediaState.Stopped);
        }
        public bool IsPlayingBGM()
        {
            return (MediaPlayer.State == MediaState.Playing);
        }

        public bool IsPausedBGM()
        {
            return (MediaPlayer.State == MediaState.Paused);
        }

        public void StopBGM()
        {
            MediaPlayer.Stop();
            currentBGM = null;
        }
        public void PlayBGM(string name)
        {
            Debug.Assert(bgms.ContainsKey(name), ErrorMessage(name));

            if (currentBGM == name)
            {
                return;
            }
            if (IsPlayingBGM())
            {
                StopBGM();
            }
            MediaPlayer.Volume = 0.5f;

            currentBGM = name;
            MediaPlayer.Play(bgms[currentBGM]);
        }
        public void ChangeBGMLoopFlag(bool loopFlag)
        {
            MediaPlayer.IsRepeating = loopFlag;
        }
        #endregion

        #region WAV関連
        public void LoadSE(string name, string filepath = "./")
        {
            if (soundEffcts.ContainsKey(name))
            {
                return;
            }
            soundEffcts.Add(name, contentManager.Load<SoundEffect>(filepath + name));
        }
        public void CreateSEInstance(string name)
        {
            if (seInstances.ContainsKey(name))
            {
                return;
            }
            Debug.Assert(soundEffcts.ContainsKey(name), "先に" + name + "の読み込み処理をしてください");

            seInstances.Add(name, soundEffcts[name].CreateInstance());
        }

        public void PlaySE(string name)
        {
            Debug.Assert(soundEffcts.ContainsKey(name), ErrorMessage(name));

            soundEffcts[name].Play();
        }
        public void PlaySEInstance(string name, bool loopFlag = false)
        {
            Debug.Assert(seInstances.ContainsKey(name), ErrorMessage(name));
            var data = seInstances[name];
            data.IsLooped = loopFlag;
            data.Play();
            sePlayList.Add(data);
        }

        public void StoppedSE()
        {
            foreach (var se in sePlayList)
            {
                if (se.State == SoundState.Playing)
                {
                    se.Stop();
                }
            }
        }
        public void PausedSE(string name)
        {
            foreach (var se in sePlayList)
            {
                if (se.State == SoundState.Playing)
                {
                    se.Pause();
                }
            }
        }
        public void RemoveSE()
        {
            sePlayList.RemoveAll(se => (se.State == SoundState.Stopped));
        }

        #endregion

        public void Unload()
        {
            bgms.Clear();
            soundEffcts.Clear();
            sePlayList.Clear();
        }
    }
}
