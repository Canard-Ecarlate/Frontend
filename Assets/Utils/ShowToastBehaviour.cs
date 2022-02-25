using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    class ShowToastBehaviour : MonoBehaviour
    {
        public void ShowToast(Canvas toastCanvas, string text, int duration)
        {
            StartCoroutine(ShowToastCor(toastCanvas, text, duration));
        }

        private IEnumerator ShowToastCor(Canvas toastCanvas, string text, int duration)
        {
            Image toastPanel = toastCanvas.GetComponent<Image>();
            Color originalPanelColor = toastPanel.color;
            Text toastText = toastCanvas.GetComponent<Transform>().Find("Text").GetComponent<Text>();
            Color originalTextColor = toastText.color;
            toastCanvas.enabled = true;
            toastPanel.enabled = true;
            toastText.text = text;
            toastText.enabled = true;

            //Fade in
            yield return FadeInAndOut(true, 0.5f, toastPanel, toastText);

            //Wait for the duration
            float counter = 0;
            while (counter < duration)
            {
                counter += Time.deltaTime;
                yield return null;
            }

            //Fade out
            yield return FadeInAndOut(false, 0.5f, toastText, toastPanel);

            toastCanvas.enabled = false;
            toastText.enabled = false;
            toastText.color = originalTextColor;
            toastPanel.enabled = false;
            toastPanel.color = originalPanelColor;
        }

        private IEnumerator FadeInAndOut(bool fadeIn, float duration, params MaskableGraphic[] targets)
        {
            //Set Values depending on if fadeIn or fadeOut
            float a, b;
            if (fadeIn)
            {
                a = 0f;
                b = 1f;
            }
            else
            {
                a = 1f;
                b = 0f;
            }

            Color[] currentColors = new Color[targets.Length];
            for (int i=0; i<targets.Length; i++)
            {
                currentColors[i]=targets[i].color;
            }
            float counter = 0f;

            while (counter < duration)
            {
                counter += Time.deltaTime;
                float alpha = Mathf.Lerp(a, b, counter / duration);
                for (int i=0; i<targets.Length; i++)
                {
                    Color c = currentColors[i];
                    targets[i].color = new Color(c.r, c.g, c.b, alpha);
                }
                yield return null;
            }
        }
    }
}