using Cysharp.Threading.Tasks;
using System;
using TEngine;
using UnityEngine;
using AudioType = TEngine.AudioType;

namespace GameLogic
{
    [Update]
    public class HomeSystem : BehaviourSingleton<HomeSystem>
    {
        private int CurrentSaveId => GameSaveManager.Instance.CurrentSaveId;

        public SettingData SettingData;

        public GameSaveData gameSaveData;

        public async UniTaskVoid LoadHome()
        {
            InitSetting();
            await UniTask.Yield();

            await ShowHomeBack();

            GameModule.UI.ShowUIAsync<UIStartWindow>();
            GameModule.Audio.Play(AudioType.Music, "start",true);
        }

        private void InitSetting()
        {
            SettingData = GameSaveManager.Instance.settingData;
            GameModule.Audio.Volume = SettingData.SoundVolume;
            GameModule.Audio.MusicVolume = SettingData.MusicVolume;
        }

        private async UniTask ShowHomeBack()
        {
            await UniTask.Yield();
            if (CurrentSaveId == -1) 
            {

            }
        }

        public void ExitHome()
        {
            GameModule.Audio.StopAll(true);
        }

        public override void Update()
        {
            
        }
    }
}
