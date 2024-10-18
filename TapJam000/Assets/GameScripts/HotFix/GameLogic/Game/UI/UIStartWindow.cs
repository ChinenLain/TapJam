using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class UIStartWindow : UIWindow
    {
        #region 脚本工具生成的代码
        private Image m_imgTitle;
        private Button m_btnStart;
        private Text m_textHint;
        private Button m_btnSetting;
        private GameObject m_goUISettingPanel;
        private Button m_btnLanguage;
        private Button m_btnLanguageSelect;
        private Slider m_sliderMusic;
        private Button m_btnMusicmin;
        private Button m_btnMusicmax;
        private Slider m_sliderSound;
        private Button m_btnSoundmin;
        private Button m_btnSoundmax;
        private Button m_btnClose;
        private GameObject m_goUILoadingPanel;

        protected override void ScriptGenerator()
        {
            m_imgTitle = FindChildComponent<Image>("m_imgTitle");
            m_btnStart = FindChildComponent<Button>("m_btnStart");
            m_textHint = FindChildComponent<Text>("m_btnStart/m_textHint");
            m_btnSetting = FindChildComponent<Button>("m_btnSetting");
            m_goUISettingPanel = FindChild("m_goUISettingPanel").gameObject;
            m_btnLanguage = FindChildComponent<Button>("m_goUISettingPanel/imgBack02/m_btnLanguage");
            m_btnLanguageSelect = FindChildComponent<Button>("m_goUISettingPanel/imgBack02/m_btnLanguageSelect");
            m_sliderMusic = FindChildComponent<Slider>("m_goUISettingPanel/imgBack02/m_sliderMusic");
            m_btnMusicmin = FindChildComponent<Button>("m_goUISettingPanel/imgBack02/m_btnMusicmin");
            m_btnMusicmax = FindChildComponent<Button>("m_goUISettingPanel/imgBack02/m_btnMusicmax");
            m_sliderSound = FindChildComponent<Slider>("m_goUISettingPanel/imgBack02/m_sliderSound");
            m_btnSoundmin = FindChildComponent<Button>("m_goUISettingPanel/imgBack02/m_btnSoundmin");
            m_btnSoundmax = FindChildComponent<Button>("m_goUISettingPanel/imgBack02/m_btnSoundmax");
            m_btnClose = FindChildComponent<Button>("m_goUISettingPanel/imgBack02/m_btnClose");
            m_goUILoadingPanel = FindChild("m_goUILoadingPanel").gameObject;

            m_btnStart.onClick.AddListener(UniTask.UnityAction(OnClickStartBtn));
            m_btnSetting.onClick.AddListener(UniTask.UnityAction(OnClickSettingBtn));
            m_btnLanguage.onClick.AddListener(UniTask.UnityAction(OnClickLanguageBtn));
            m_btnLanguageSelect.onClick.AddListener(UniTask.UnityAction(OnClickLanguageSelectBtn));
            m_sliderMusic.onValueChanged.AddListener(OnSliderMusicChange);
            m_btnMusicmin.onClick.AddListener(UniTask.UnityAction(OnClickMusicminBtn));
            m_btnMusicmax.onClick.AddListener(UniTask.UnityAction(OnClickMusicmaxBtn));
            m_sliderSound.onValueChanged.AddListener(OnSliderSoundChange);
            m_btnSoundmin.onClick.AddListener(UniTask.UnityAction(OnClickSoundminBtn));
            m_btnSoundmax.onClick.AddListener(UniTask.UnityAction(OnClickSoundmaxBtn));
            m_btnClose.onClick.AddListener(UniTask.UnityAction(OnClickCloseBtn));
        }
        #endregion

        protected override void RegisterEvent()
        {
            
        }

        protected override void OnRefresh()
        {
            m_goUISettingPanel.SetActive(false);
            m_goUILoadingPanel.SetActive(false);
        }

        #region 事件
        private async UniTaskVoid OnClickStartBtn()
        {
            await UniTask.Yield();
            m_goUILoadingPanel.SetActive(true);
            await GameModule.Scene.LoadScene("scene_game").ToUniTask();
            GameSystem.Instance.LoadGame().Forget();
            GameModule.UI.CloseUI<UIStartWindow>();
            
        }
        private async UniTaskVoid OnClickSettingBtn()
        {
            await UniTask.Yield();
            m_goUISettingPanel.SetActive(true);
            m_btnSetting.gameObject.SetActive(false);
        }
        private async UniTaskVoid OnClickLanguageBtn()
        {
            await UniTask.Yield();
        }
        private async UniTaskVoid OnClickLanguageSelectBtn()
        {
            await UniTask.Yield();
        }

        private void OnSliderMusicChange(float value)
        {
            GameModule.Audio.MusicVolume = value;
        }

        private async UniTaskVoid OnClickMusicminBtn()
        {
            await UniTask.Yield();
            m_sliderMusic.value = 0;
        }
        private async UniTaskVoid OnClickMusicmaxBtn()
        {
            await UniTask.Yield();
            m_sliderMusic.value = 1;
        }

        private void OnSliderSoundChange(float value)
        {
            GameModule.Audio.SoundVolume = value;
        }

        private async UniTaskVoid OnClickSoundminBtn()
        {
            await UniTask.Yield();
            m_sliderSound.value = 0;
        }
        private async UniTaskVoid OnClickSoundmaxBtn()
        {
            await UniTask.Yield();
            m_sliderSound.value = 1;
        }
        private async UniTaskVoid OnClickCloseBtn()
        {
            await UniTask.Yield();
            m_btnSetting.gameObject.SetActive(true);
            m_goUISettingPanel.SetActive(false);
        }
        #endregion

    }
}


