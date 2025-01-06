using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Refrigerator
{
    public class RefrigeratorProductClickHandler : MonoBehaviour, IPointerClickHandler
    {
        public event Action LeftButtonClicked;
        public event Action RightButtonClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                LeftButtonClicked?.Invoke();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                RightButtonClicked?.Invoke();
            }
        }
    }
}