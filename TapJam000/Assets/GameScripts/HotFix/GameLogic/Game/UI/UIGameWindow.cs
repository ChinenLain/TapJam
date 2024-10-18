using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class UIGameWindow : UIWindow
    {
        private UIJoystickWidget m_joystickWidget;

        #region �ű��������ɵĴ���
        private GameObject m_itemJoystickWidget;
        protected override void ScriptGenerator()
        {
            m_itemJoystickWidget = FindChild("m_itemJoystickWidget").gameObject;
        }
        #endregion

        #region �¼�

        #endregion

        protected override void BindMemberProperty()
        {
            m_joystickWidget = CreateWidget<UIJoystickWidget>(m_itemJoystickWidget);
        }
    }
}
