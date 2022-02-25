using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CustomHandlers
{
    public class OnClickHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UnityEvent OnClick;

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            OnClick.Invoke();
        }
    }
}