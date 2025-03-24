using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    class UIActorAttributeWidget : UIWidget
    {
        #region 脚本工具生成的代码
        private Button m_btnAvatar;
        private RectTransform m_rectBlood;
        private RectTransform m_rectBloodmask;
        private RectTransform m_rectBloodFluid;
        private RectTransform m_rectMagic;
        private RectTransform m_rectMagicmask;
        private RectTransform m_rectMagicFluid;
        protected override void ScriptGenerator()
        {
            m_btnAvatar = FindChildComponent<Button>("m_btnAvatar");
            m_rectBlood = FindChildComponent<RectTransform>("m_rectBlood");
            m_rectBloodmask = FindChildComponent<RectTransform>("m_rectBlood/m_rectBloodmask");
            m_rectBloodFluid = FindChildComponent<RectTransform>("m_rectBlood/m_rectBloodmask/m_rectBloodFluid");
            m_rectMagic = FindChildComponent<RectTransform>("m_rectMagic");
            m_rectMagicmask = FindChildComponent<RectTransform>("m_rectMagic/m_rectMagicmask");
            m_rectMagicFluid = FindChildComponent<RectTransform>("m_rectMagic/m_rectMagicmask/m_rectMagicFluid");
            m_btnAvatar.onClick.AddListener(UniTask.UnityAction(OnClickAvatarBtn));
        }
        #endregion

        #region 事件
        private async UniTaskVoid OnClickAvatarBtn()
        {
            GameEvent.Send(UIEventDefine.AvatarClick);
            await UniTask.Yield();
        }
        #endregion

        protected override void RegisterEvent()
        {
            AddUIEvent<float,float>(UIEventDefine.ActorBloodChanged,OnBloodChanged);
        }

        private void OnBloodChanged(float blood,float Maxblood)
        {
            m_rectBloodFluid.anchoredPosition = new Vector2(0, (- (100 - blood) / Maxblood) * m_rectBloodFluid.rect.height);
        }
    }
}
