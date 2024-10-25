using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;
using TMPro;

namespace GameLogic
{
    class UIDialogueWidget : UIWidget
    {
        private DialogueCtr dialogueCtr;

        #region 脚本工具生成的代码
        private Image m_imgPersonLeft;
        private Image m_imgPersonRight;
        private Image m_imgBg;
        private TextMeshProUGUI m_tmptextName;
        private TextMeshProUGUI m_tmptextTalk;
        protected override void ScriptGenerator()
        {
            m_imgPersonLeft = FindChildComponent<Image>("m_imgPersonLeft");
            m_imgPersonRight = FindChildComponent<Image>("m_imgPersonRight");
            m_imgBg = FindChildComponent<Image>("m_imgBg");
            m_tmptextName = FindChildComponent<TextMeshProUGUI>("m_imgBg/m_tmptextName");
            m_tmptextTalk = FindChildComponent<TextMeshProUGUI>("m_imgBg/m_tmptextTalk");
        }
        #endregion

        #region 事件
        #endregion

        protected override void OnCreate()
        {
            AddComponent();
        }

        protected override void OnRefresh()
        {
            AddComponent();
        }

        private void AddComponent()
        {
            dialogueCtr = gameObject.GetComponent<DialogueCtr>();
            if (dialogueCtr == null)
            {
                dialogueCtr = gameObject.AddComponent<DialogueCtr>();

            }
        }
    }
}
