using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class UIGameWindow : UIWindow
    {
        private UIJoystickWidget _joystickWidget;
        private UIAttackWidget _attackWidget;
        private UIDialogueWidget _dialogueWidget;

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

        private GameObject m_itemUIDialogueWidget;
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

            m_itemUIDialogueWidget = FindChild("m_itemUIDialogueWidget").gameObject;

            m_btnMap.onClick.AddListener(UniTask.UnityAction(OnClickMapBtn));
            m_btnProp.onClick.AddListener(UniTask.UnityAction(OnClickPropBtn));
            m_btnEquipment.onClick.AddListener(UniTask.UnityAction(OnClickEquipmentBtn));
        }
        #endregion

        #region 事件
        private async UniTaskVoid OnClickMapBtn()
        {
            await UniTask.Yield();
        }
        private async UniTaskVoid OnClickPropBtn()
        {
            await UniTask.Yield();
        }
        private async UniTaskVoid OnClickEquipmentBtn()
        {
            await UniTask.Yield();
        }
        #endregion

        protected override void OnCreate()
        {
            imgBtnMapDown = GameModule.Resource.LoadAsset<Sprite>("map-button-active");
            imgBtnMapUp = GameModule.Resource.LoadAsset<Sprite>("map-button-close");
            imgBtnPropDown = GameModule.Resource.LoadAsset<Sprite>("prop-button-active");
            imgBtnPropUp = GameModule.Resource.LoadAsset<Sprite>("prop-button-close");
            imgBtnEqDown = GameModule.Resource.LoadAsset<Sprite>("eq-active");
            imgBtnEqUp = GameModule.Resource.LoadAsset<Sprite>("eq-close");
            m_goUIGameInfoPanel.gameObject.SetActive(false);
            m_itemUIDialogueWidget.gameObject.SetActive(false);
        }

        protected override void OnRefresh()
        {
            
        }

        protected override void BindMemberProperty()
        {
            _joystickWidget = CreateWidget<UIJoystickWidget>(m_itemJoystickWidget);
            _attackWidget = CreateWidget<UIAttackWidget>(m_itemAttackWidget);
            _dialogueWidget = CreateWidget<UIDialogueWidget>(m_itemUIDialogueWidget);
        }

        private void OpenGameInfoPanel()
        {

        }
    }
}