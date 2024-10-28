using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using TEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class DialogueCtr : MonoBehaviour
    {
        public TextMeshProUGUI talktext;

        private string TempText;
        private string CorText;
        private float TextSpeed;
        private bool ShowTalkText;

        public void Init(TextMeshProUGUI talktext,float speed)
        {
            ShowTalkText = false;
            this.talktext = talktext;
            TextSpeed = speed;
            GameEvent.AddEventListener(UIEventDefine.DialogueClick, OnDialogueClick);
        }

        private void OnDialogueClick()
        {
            if (ShowTalkText)
            {
                StopCoroutine("ShowDialogueCoroutine");
                DisplayAllText();
                ShowTalkText = false;
            }
            else
            {
                GameEvent.Send(UIEventDefine.PlayTalkFinish);
            }
        }

        public void SetSpeed(float speed)
        {
            TextSpeed = speed;
        }

        public void PlayText(string text)
        {
            CorText = text;
            StartCoroutine("ShowDialogueCoroutine");
        }

        IEnumerator ShowDialogueCoroutine()
        {
            yield return new WaitForEndOfFrame();
            TempText = "";
            ShowTalkText = true;
            for (int i = 0; i < CorText.Length; i++)
            {
                TempText += CorText[i];
                talktext.text = TempText;
                yield return new WaitForSeconds(1 / TextSpeed);
            }
            ShowTalkText = false;
        }

        
        private void DisplayAllText()
        {
            talktext.text = CorText;
        }
    }
}
