using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CanardEcarlate.CustomHandlers
{
    public class onClickHandler : MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent onClick;

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            onClick.Invoke();
        }
    }
}