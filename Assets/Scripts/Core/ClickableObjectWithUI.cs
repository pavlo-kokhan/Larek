using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class ClickableObjectWithUI : MonoBehaviour, IPointerClickHandler
    {
        public event Action PanelOpened;
        
        [SerializeField] private GameObject _interactionUI;
        
        private bool _isActiveUI;

        private void Update()
        {
            if (_isActiveUI && Input.GetKeyUp(KeyCode.Escape))
            {
                ToggleUI(false);
            }
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                ToggleUI(true);
                PanelOpened?.Invoke();
            }
        }
        
        private void ToggleUI(bool isActive)
        {
            _interactionUI.SetActive(isActive);
            _isActiveUI = isActive;
        }
        
        public void ClosePanel()
        {
            ToggleUI(false);
        }
    }
}