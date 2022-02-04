using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CanardEcarlate.CustomHandlers
{
    public class onClickHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] public UnityEvent onClick;

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            onClick.Invoke();
        }
    }
}