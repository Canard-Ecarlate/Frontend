using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CanardEcarlate.Utils
{
    static class ShowToast
    {
        public static void toast(MonoBehaviour scene, Text toastText, string text, int duration=3)
        {
            ShowToastBehaviour st = scene.gameObject.AddComponent(typeof(ShowToastBehaviour)) as ShowToastBehaviour;
            st.showToast(toastText,text,duration);
        }
    }
    
    class ShowToastBehaviour : MonoBehaviour
    {
        public void showToast(Text toastText, string text, int duration)
        {
            StartCoroutine(showToastCOR(toastText, text, duration));
        }

        private IEnumerator showToastCOR(Text toastText, string text, int duration)
        {
            Color orginalColor = toastText.color;

            toastText.text = text;
            toastText.enabled = true;

            //Fade in
            yield return fadeInAndOut(toastText, true, 0.5f);

            //Wait for the duration
            float counter = 0;
            while (counter < duration)
            {
                counter += Time.deltaTime;
                yield return null;
            }

            //Fade out
            yield return fadeInAndOut(toastText, false, 0.5f);

            toastText.enabled = false;
            toastText.color = orginalColor;
        }

        private IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
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

            Color currentColor = Color.clear;
            float counter = 0f;

            while (counter < duration)
            {
                counter += Time.deltaTime;
                float alpha = Mathf.Lerp(a, b, counter / duration);

                targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                yield return null;
            }
        }
    }
}