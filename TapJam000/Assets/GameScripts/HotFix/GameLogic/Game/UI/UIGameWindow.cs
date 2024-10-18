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

        #region 脚本工具生成的代码
        private GameObject m_itemJoystickWidget;
        protected override void ScriptGenerator()
        {
            m_itemJoystickWidget = FindChild("m_itemJoystickWidget").gameObject;
        }
        #endregion

        #region 事件

        #endregion

        protected override void BindMemberProperty()
        {
            m_joystickWidget = CreateWidget<UIJoystickWidget>(m_itemJoystickWidget);
        }
    }
}
