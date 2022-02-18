using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CanardEcarlate.Utils
{
    class ShowToastBehaviour : MonoBehaviour
    {
        public void showToast(Canvas toastCanvas, string text, int duration)
        {
            StartCoroutine(showToastCOR(toastCanvas, text, duration));
        }

        private IEnumerator showToastCOR(Canvas toastCanvas, string text, int duration)
        {
            Image toastPanel = toastCanvas.GetComponent<Image>();
            Color originalPanelColor = toastPanel.color;
            Text toastText = toastCanvas.GetComponent<Transform>().Find("Text").GetComponent<Text>();
            Color originalTextColor = toastText.color;

            toastPanel.enabled = true;
            toastText.text = text;
            toastText.enabled = true;

            //Fade in
            yield return fadeInAndOut(true, 0.5f, toastPanel, toastText);

            //Wait for the duration
            float counter = 0;
            while (counter < duration)
            {
                counter += Time.deltaTime;
                yield return null;
            }

            //Fade out
            yield return fadeInAndOut(false, 0.5f, toastText, toastPanel);

            toastText.enabled = false;
            toastText.color = originalTextColor;
            toastPanel.enabled = false;
            toastPanel.color = originalPanelColor;
        }

        private IEnumerator fadeInAndOut(bool fadeIn, float duration, params MaskableGraphic[] targets)
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