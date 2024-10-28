using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;
using TMPro;
using System;
using System.IO;
using System.Reflection;

namespace GameLogic
{
    class UIDialogueWidget : UIWidget
    {
        private float talk_speed;
        private bool normal_speed;
        private DialogueCtr dialogueCtr;

        private bool hasTask;
        private int TaskId;

        public string[] texts;
        private int current_line;
        private int end_line;

        #region 脚本工具生成的代码
        private Image m_imgPersonLeft;
        private Image m_imgPersonRight;
        private Button m_btnBg;
        private TextMeshProUGUI m_tmptextName;
        private TextMeshProUGUI m_tmptextTalk;
        private Button m_btnSpeed;
        private Button m_btnSkip;
        protected override void ScriptGenerator()
        {
            m_imgPersonLeft = FindChildComponent<Image>("m_imgPersonLeft");
            m_imgPersonRight = FindChildComponent<Image>("m_imgPersonRight");
            m_btnBg = FindChildComponent<Button>("m_btnBg");
            m_tmptextName = FindChildComponent<TextMeshProUGUI>("m_btnBg/m_tmptextName");
            m_tmptextTalk = FindChildComponent<TextMeshProUGUI>("m_btnBg/m_tmptextTalk");
            m_btnSpeed = FindChildComponent<Button>("m_btnSpeed");
            m_btnSkip = FindChildComponent<Button>("m_btnSkip");
            m_btnBg.onClick.AddListener(UniTask.UnityAction(OnClickBgBtn));
            m_btnSpeed.onClick.AddListener(UniTask.UnityAction(OnClickSpeedBtn));
            m_btnSkip.onClick.AddListener(UniTask.UnityAction(OnClickSkipBtn));
        }
        #endregion

        #region 事件
        private async UniTaskVoid OnClickBgBtn()
        {
            await UniTask.Yield();
            GameEvent.Send(UIEventDefine.DialogueClick);
        }
        private async UniTaskVoid OnClickSpeedBtn()
        {
            await UniTask.Yield();
            if (normal_speed)
            {
                talk_speed = 25.0f;
                normal_speed = false;
            }
            else
            {
                talk_speed = 15.0f;
                normal_speed = true;
            }
            dialogueCtr.SetSpeed(talk_speed);
        }
        private async UniTaskVoid OnClickSkipBtn()
        {
            await UniTask.Yield();
            GameEvent.Send(UIEventDefine.DialogueClick);
        }
        #endregion

        protected override void RegisterEvent()
        {
            AddUIEvent(UIEventDefine.PlayTalkFinish, OnPlayTalkFinish);
            AddUIEvent<int,string,int,int>(GameEventDefine.DialogueTaskStart, StartDialogueTask);
        }

        

        protected override void OnCreate()
        {
            AddComponent();
            talk_speed = 15.0f;
            dialogueCtr.SetSpeed(talk_speed);
            normal_speed = true;
            hasTask = false;
            m_imgPersonLeft.gameObject.SetActive(false);
            m_imgPersonRight.gameObject.SetActive(false);
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
                dialogueCtr.Init(m_tmptextTalk,talk_speed);
            }
        }

        private void StartDialogueTask(int TaskId,string filename,int start_line,int end_line)
        {
            if (hasTask) return;
            this.TaskId = TaskId;
            current_line = start_line;
            this.end_line = end_line;
            texts = GameModule.Resource.LoadAsset<TextAsset>(filename).text.Split(
                new[] {"\r\n","\r","\n"},System.StringSplitOptions.None);
            gameObject.SetActive(true);
            ParsingText(current_line);
        }

        private void OnPlayTalkFinish()
        {
            current_line++;
            ParsingText(current_line);
        }

        private void ParsingText(int line)
        {
            if (line > end_line)
            {
                GameEvent.Send(GameEventDefine.DialogueTaskFinish,TaskId);
                hasTask = false;
                gameObject.SetActive(false);
                return;
            }
            string[] cur = texts[current_line].Split(':');
            switch (cur[0])
            {
                case "!":
                    //"!:"是注释
                    break;
                case "setleft":
                    SetPerson("left", cur[1]);
                    break;
                case "setright":
                    SetPerson("right", cur[1]);
                    break;
                case "closeleft":
                    ClosePerson("left");
                    break;
                case "closeright":
                    ClosePerson("right");
                    break;
                case "closeall":
                    ClosePerson("left");
                    ClosePerson("right");
                    break;
                case "setbgm":
                    GameModule.Audio.Play(TEngine.AudioType.Music, cur[1]);
                    break;
                case "setsound":
                    GameModule.Audio.Play(TEngine.AudioType.Sound, cur[1]);
                    break;
                default:
                    m_tmptextName.text = cur[0];
                    dialogueCtr.PlayText(cur[1]);
                    return;
            }
            current_line++;
            ParsingText(current_line);
        }

        private void SetPerson(string pos,string filename)
        {
            switch (pos)
            {
                case "left":
                    m_imgPersonLeft.gameObject.SetActive(true);
                    m_imgPersonLeft.color = new Color(1, 1, 1,1);
                    m_imgPersonLeft.sprite = GameModule.Resource.LoadAsset<Sprite>(filename);
                    m_imgPersonRight.color = new Color(0.7f, 0.7f, 0.7f, 1);
                    break;
                case "right":
                    m_imgPersonRight.gameObject.SetActive(true);
                    m_imgPersonRight.color = new Color(1, 1, 1, 1);
                    m_imgPersonRight.sprite = GameModule.Resource.LoadAsset<Sprite>(filename);
                    m_imgPersonLeft.color = new Color(0.7f, 0.7f, 0.7f, 1);
                    break;
            }
        }

        private void ClosePerson(string pos)
        {
            switch (pos)
            {
                case "left":
                    m_imgPersonLeft.gameObject?.SetActive(false);
                    break;
                case "right":
                    m_imgPersonRight.gameObject?.SetActive(false);
                    break;
            }
        }
    }
}
