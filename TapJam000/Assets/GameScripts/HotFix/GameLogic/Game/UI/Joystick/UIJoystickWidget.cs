using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;
using System;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class UIJoystickWidget : UIWidget
    {
        private MoveJoystick moveJoystick;

        Sprite imgHandle;
        Sprite imgHandleDrag;

        #region 脚本工具生成的代码
        private Image m_imgBackground;
        private Image m_imgHandle;
        protected override void ScriptGenerator()
        {
            m_imgBackground = FindChildComponent<Image>("m_imgBackground");
            m_imgHandle = FindChildComponent<Image>("m_imgBackground/m_imgHandle");
        }
        #endregion

        #region 事件

        #endregion

        protected override void RegisterEvent()
        {
            AddUIEvent(UIEventDefine.StickDrag, onStickDrag);
            AddUIEvent(UIEventDefine.StickEndDrag, onStickEndDrag);
        }


        private void onStickDrag()
        {
            m_imgHandle.sprite = imgHandleDrag;
        }

        private void onStickEndDrag()
        {
           m_imgHandle.sprite = imgHandle;
        }

        protected override void OnCreate()
        {
            imgHandle = GameModule.Resource.LoadAsset<Sprite>("move-2-01");
            imgHandleDrag = GameModule.Resource.LoadAsset<Sprite>("move-2-02");
            AddComponent();
        }

        protected override void OnRefresh()
        {
            AddComponent();
        }

        private void AddComponent()
        {
            moveJoystick = gameObject.GetComponent<MoveJoystick>();
            if (moveJoystick == null)
            {
                moveJoystick = gameObject.AddComponent<MoveJoystick>();
                moveJoystick.Init(m_imgBackground.gameObject.GetComponent<RectTransform>(),
                    m_imgHandle.gameObject.GetComponent<RectTransform>());
            }
        }
    }
}
