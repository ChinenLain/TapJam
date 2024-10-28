using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class UIGameWindow : UIWindow
    {
        private bool isdead = false;

        private UIJoystickWidget _joystickWidget;
        private UIAttackWidget _attackWidget;
        private UIDialogueWidget _dialogueWidget;
        private UIActorAttributeWidget _actorAttributeWidget;

        private Sprite imgBtnMapDown;
        private Sprite imgBtnPropDown;
        private Sprite imgBtnEqDown;
        private Sprite imgBtnMapUp;
        private Sprite imgBtnPropUp;
        private Sprite imgBtnEqUp;

        #region 脚本工具生成的代码
        private GameObject m_itemJoystickWidget;
        private GameObject m_itemActorAttributeWidget;
        private GameObject m_itemAttackWidget;

        private GameObject m_goUIGameInfoPanel;
        private GameObject m_itemUIMapWidget;
        private GameObject m_itemUIPropWidget;
        private GameObject m_itemUIEquipmentWidget;
        private Button m_btnMap;
        private Button m_btnProp;
        private Button m_btnEquipment;
        private Button m_btnBack;
        private Button m_btnRestart;

        private GameObject m_itemUIDialogueWidget;
        private GameObject m_goLoading;
        private GameObject m_goDead;
        protected override void ScriptGenerator()
        {
            m_itemJoystickWidget = FindChild("m_itemJoystickWidget").gameObject;
            m_itemActorAttributeWidget = FindChild("m_itemActorAttributeWidget").gameObject;
            m_itemAttackWidget = FindChild("m_itemAttackWidget").gameObject;

            m_goUIGameInfoPanel = FindChild("m_goUIGameInfoPanel").gameObject;
            m_itemUIMapWidget = FindChild("m_goUIGameInfoPanel/Widgets/m_itemUIMapWidget").gameObject;
            m_itemUIPropWidget = FindChild("m_goUIGameInfoPanel/Widgets/m_itemUIPropWidget").gameObject;
            m_itemUIEquipmentWidget = FindChild("m_goUIGameInfoPanel/Widgets/m_itemUIEquipmentWidget").gameObject;
            m_btnMap = FindChildComponent<Button>("m_goUIGameInfoPanel/Buttons/m_btnMap");
            m_btnProp = FindChildComponent<Button>("m_goUIGameInfoPanel/Buttons/m_btnProp");
            m_btnEquipment = FindChildComponent<Button>("m_goUIGameInfoPanel/Buttons/m_btnEquipment");
            m_btnBack = FindChildComponent<Button>("m_goUIGameInfoPanel/m_btnBack");
            m_btnRestart = FindChildComponent<Button>("m_goDead/m_btnRestart");

            m_itemUIDialogueWidget = FindChild("m_itemUIDialogueWidget").gameObject;
            m_goLoading = FindChild("m_goLoading").gameObject;
            m_goDead = FindChild("m_goDead").gameObject;

            m_btnMap.onClick.AddListener(UniTask.UnityAction(OnClickMapBtn));
            m_btnProp.onClick.AddListener(UniTask.UnityAction(OnClickPropBtn));
            m_btnEquipment.onClick.AddListener(UniTask.UnityAction(OnClickEquipmentBtn));
            m_btnBack.onClick.AddListener(UniTask.UnityAction(OnClickBackBtn));
            m_btnRestart.onClick.AddListener(UniTask.UnityAction(OnClickRestartBtn));
        }

        

        #endregion

        #region 事件
        private async UniTaskVoid OnClickMapBtn()
        {
            await UniTask.Yield();
            m_btnMap.gameObject.GetComponent<Image>().sprite = imgBtnMapDown;
            m_itemUIMapWidget.SetActive(true);
            m_btnProp.gameObject.GetComponent<Image>().sprite = imgBtnPropUp;
            m_itemUIPropWidget.SetActive(false);
            m_btnEquipment.gameObject.GetComponent<Image>().sprite = imgBtnEqUp;
            m_itemUIEquipmentWidget.SetActive(false);
        }
        private async UniTaskVoid OnClickPropBtn()
        {
            await UniTask.Yield();
            m_btnMap.gameObject.GetComponent<Image>().sprite = imgBtnMapUp;
            m_itemUIMapWidget.SetActive(false);
            m_btnProp.gameObject.GetComponent<Image>().sprite = imgBtnPropDown;
            m_itemUIPropWidget.SetActive(true);
            m_btnEquipment.gameObject.GetComponent<Image>().sprite = imgBtnEqUp;
            m_itemUIEquipmentWidget.SetActive(false);

        }
        private async UniTaskVoid OnClickEquipmentBtn()
        {
            await UniTask.Yield();
            m_btnMap.gameObject.GetComponent<Image>().sprite = imgBtnMapUp;
            m_itemUIMapWidget.SetActive(false);
            m_btnProp.gameObject.GetComponent<Image>().sprite = imgBtnPropUp;
            m_itemUIPropWidget.SetActive(false);
            m_btnEquipment.gameObject.GetComponent<Image>().sprite = imgBtnEqDown;
            m_itemUIEquipmentWidget.SetActive(true);

        }

        private async UniTaskVoid OnClickBackBtn()
        {
            await UniTask.Yield();
            m_goUIGameInfoPanel.SetActive(false);
        }

        private async UniTaskVoid OnClickRestartBtn()
        {
            await GameModule.Scene.LoadScene("scene_start").ToUniTask();
            HomeSystem.Instance.LoadHome();
            GameModule.UI.CloseUI<UIGameWindow>();
        }
        #endregion

        protected override void RegisterEvent()
        {
            AddUIEvent(UIEventDefine.AvatarClick,OpenGameInfoPanel);
            AddUIEvent(GameEventDefine.ChangeMap,OnChangeMap);
            AddUIEvent(GameEventDefine.LoadMapFinish, OnLoadMapFinish);
            AddUIEvent(ActorEventDefine.Dead, OnActorDead);
        }

        private void OnActorDead()
        {
            if(!isdead)
            {
                m_goDead.gameObject.SetActive(true);
                m_goDead.GetComponent<LoadingBhv>().Open(0.4f);
                isdead = true;
            }
        }

        private void OnChangeMap()
        {
            m_goLoading.SetActive(true);
            m_goLoading.GetComponent<LoadingBhv>().Open();
        }

        private void OnLoadMapFinish()
        {
            m_goLoading.GetComponent<LoadingBhv>().Close();
        }

        protected override void OnCreate()
        {
            isdead = false;
            imgBtnMapDown = GameModule.Resource.LoadAsset<Sprite>("map-button-active");
            imgBtnMapUp = GameModule.Resource.LoadAsset<Sprite>("map-button-close");
            imgBtnPropDown = GameModule.Resource.LoadAsset<Sprite>("prop-button-active");
            imgBtnPropUp = GameModule.Resource.LoadAsset<Sprite>("prop-button-close");
            imgBtnEqDown = GameModule.Resource.LoadAsset<Sprite>("eq-active");
            imgBtnEqUp = GameModule.Resource.LoadAsset<Sprite>("eq-close");
            m_goUIGameInfoPanel.gameObject.SetActive(false);
            m_itemUIDialogueWidget.gameObject.SetActive(false);
            m_goLoading.gameObject.SetActive(false);
            m_goDead.gameObject.SetActive(false);
            AddComponent();
        }

        protected override void OnRefresh()
        {
            isdead = false;
            AddComponent();
        }

        protected override void BindMemberProperty()
        {
            _joystickWidget = CreateWidget<UIJoystickWidget>(m_itemJoystickWidget);
            _attackWidget = CreateWidget<UIAttackWidget>(m_itemAttackWidget);
            _dialogueWidget = CreateWidget<UIDialogueWidget>(m_itemUIDialogueWidget);
            _actorAttributeWidget = CreateWidget<UIActorAttributeWidget>(m_itemActorAttributeWidget);
        }

        private void AddComponent()
        {
            LoadingBhv loadingBhv = m_goLoading.GetComponent<LoadingBhv>();
            if (loadingBhv == null)
            {
                loadingBhv = m_goLoading.AddComponent<LoadingBhv>();
                loadingBhv.Init(m_goLoading.GetComponent<Image>());
            }

            LoadingBhv loadingBhv2 = m_goDead.GetComponent<LoadingBhv>();
            if (loadingBhv2 == null)
            {
                loadingBhv2 = m_goDead.AddComponent<LoadingBhv>();
                loadingBhv2.Init(m_goDead.GetComponent<Image>());
            }
        }

        private void OpenGameInfoPanel()
        {
            m_goUIGameInfoPanel.SetActive(true);
            m_btnMap.gameObject.GetComponent<Image>().sprite = imgBtnMapDown;
            m_itemUIMapWidget.SetActive(true);
            m_btnProp.gameObject.GetComponent<Image>().sprite = imgBtnPropUp;
            m_itemUIPropWidget.SetActive(false);
            m_btnEquipment.gameObject.GetComponent<Image>().sprite = imgBtnEqUp;
            m_itemUIEquipmentWidget.SetActive(false);
        }
    }
}