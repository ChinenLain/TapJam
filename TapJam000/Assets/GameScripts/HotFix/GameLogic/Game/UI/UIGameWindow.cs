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

        #region 脚本工具生成的代码
        private GameObject m_itemJoystickWidget;
        private GameObject m_itemActorAttributeWidget;
        private GameObject m_itemAttackWidget;
        protected override void ScriptGenerator()
        {
            m_itemJoystickWidget = FindChild("m_itemJoystickWidget").gameObject;
            m_itemActorAttributeWidget = FindChild("m_itemActorAttributeWidget").gameObject;
            m_itemAttackWidget = FindChild("m_itemAttackWidget").gameObject;
        }
        #endregion

        #region 事件
        #endregion

        protected override void BindMemberProperty()
        {
            _joystickWidget = CreateWidget<UIJoystickWidget>(m_itemJoystickWidget);
            _attackWidget = CreateWidget<UIAttackWidget>(m_itemAttackWidget);
        }
    }
}
