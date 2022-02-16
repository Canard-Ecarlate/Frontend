using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CanardEcarlate.Utils
{
    static class ShowToast
    {
        public static void toast(MonoBehaviour element, Canvas toastCanvas, string text, int duration=3)
        {
            ShowToastBehaviour st = element.gameObject.AddComponent<ShowToastBehaviour>();
            st.showToast(toastCanvas,text,duration);
        }
    }
}