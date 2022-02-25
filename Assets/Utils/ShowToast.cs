using UnityEngine;

namespace Utils
{
    static class ShowToast
    {
        public static void Toast(MonoBehaviour element, Canvas toastCanvas, string text, int duration=3)
        {
            ShowToastBehaviour st = element.gameObject.AddComponent<ShowToastBehaviour>();
            st.ShowToast(toastCanvas,text,duration);
        }
    }
}