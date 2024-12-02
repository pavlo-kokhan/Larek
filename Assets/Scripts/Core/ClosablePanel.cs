using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class ClosablePanel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ClickableObjectWithUI clickableObjectWithUI;
        
        private RectTransform _rectTransform;
        private Vector3 _startPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _startPosition = _rectTransform.anchoredPosition;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                clickableObjectWithUI.ClosePanel();
                _rectTransform.anchoredPosition = _startPosition;
            }
        }
    }
}