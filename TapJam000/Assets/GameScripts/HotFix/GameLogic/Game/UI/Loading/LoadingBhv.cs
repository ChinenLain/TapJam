using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;
using System.Collections;

namespace GameLogic
{
    class LoadingBhv : MonoBehaviour
    {
        Image m_image;
        public float fadeInDuration = 0.02f;
        public float fadeOutDuration = 0.8f;
        bool opening = false;
        bool closing = false;
        bool wantclose = false;
        public void Init(Image image) 
        {
            m_image = image;
        }

        public void Open(float speed = 0.02f)
        {
            fadeInDuration = speed;
            if(opening) return; 
            if(closing)
            {
                StopCoroutine("FadeOut");
                closing = false;
            }    
            else
            {
                m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, 0);
            } 
            StartCoroutine("FadeIn");
            opening = true;
        }

        public void Close()
        {
            if (closing) return;
            if(opening)
            {
                wantclose = true;
                return;
            }
            else
            {
                m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, 1);
            }
            StartCoroutine("FadeOut");
            closing = true;
        }

        private IEnumerator FadeOut()
        {
            Color startColor = m_image.color;
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
            float time = 0;

            yield return new WaitForSeconds(0.8f);

            while (time < fadeOutDuration)
            {
                time += Time.deltaTime;
                m_image.color = Color.Lerp(startColor, endColor, time / fadeOutDuration);
                yield return null;
            }

            m_image.color = endColor;

            closing = false;
            m_image.gameObject.SetActive(false);
        }

        private IEnumerator FadeIn()
        {
            Color startColor = m_image.color;
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1);
            float time = 0;

            while (time < fadeInDuration)
            {
                time += Time.deltaTime;
                m_image.color = Color.Lerp(startColor, endColor, time / fadeInDuration);
                yield return null;
            }

            m_image.color = endColor;

            opening = false;
            if (wantclose)
            {
                StartCoroutine("FadeOut");
                wantclose = false;
            }
        }
    }
}

