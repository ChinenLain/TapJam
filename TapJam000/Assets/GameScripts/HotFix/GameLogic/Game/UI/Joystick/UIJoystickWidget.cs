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
        private int joystickType;
        Sprite imgHandle;
        Sprite imgHandleDrag;
        Sprite imgHandle_2;
        Sprite imgHandleDrag_2;

        #region 脚本工具生成的代码
        private Image m_imgBackground;
        private Image m_imgHandle;
        private Image m_imgJoystick2;
        protected override void ScriptGenerator()
        {
            m_imgBackground = FindChildComponent<Image>("m_imgBackground");
            m_imgHandle = FindChildComponent<Image>("m_imgBackground/m_imgHandle");
            m_imgJoystick2 = FindChildComponent<Image>("m_imgJoystick2");
        }
        #endregion

        #region 事件

        #endregion

        protected override void RegisterEvent()
        {
            AddUIEvent<Vector2>(UIEventDefine.StickDrag, onStickDrag);
            AddUIEvent<Vector2>(UIEventDefine.StickEndDrag, onStickEndDrag);
        }


        private void onStickDrag(Vector2 input)
        {
            m_imgHandle.sprite = imgHandleDrag;
        }

        private void onStickEndDrag(Vector2 input)
        {
           m_imgHandle.sprite = imgHandle;
        }

        protected override void OnCreate()
        {
            imgHandle = GameModule.Resource.LoadAsset<Sprite>("move-2-01");
            imgHandleDrag = GameModule.Resource.LoadAsset<Sprite>("move-2-02");
            imgHandle_2 = GameModule.Resource.LoadAsset<Sprite>("move-1-01");
            imgHandleDrag_2 = GameModule.Resource.LoadAsset<Sprite>("move-1-02");
            joystickType = GameSaveManager.Instance.settingData.joystickType;
            if (joystickType == (int)JoystickType.Fixed) 
            {
                m_imgBackground.gameObject.SetActive(true);
                m_imgJoystick2.gameObject.SetActive(false);
            }
            else
            {
                m_imgBackground.gameObject.SetActive(false);
                m_imgJoystick2.gameObject.SetActive(false);
            }
                AddComponent();
        }

        protected override void OnRefresh()
        {
            if (joystickType == (int)JoystickType.Fixed)
            {
                m_imgBackground.gameObject.SetActive(true);
                m_imgJoystick2.gameObject.SetActive(false);
            }
            else
            {
                m_imgBackground.gameObject.SetActive(false);
                m_imgJoystick2.gameObject.SetActive(false);
            }
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
