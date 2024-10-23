using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    [RequireComponent(typeof(Collider2D))]
    public class ClickableObjectWithUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject interactionUI;
        
        private bool _isActiveUI;

        protected virtual void Update()
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
            }
        }
        
        private void ToggleUI(bool isActive)
        {
            interactionUI.SetActive(isActive);
            _isActiveUI = isActive;
        }
        
        public void ClosePanel()
        {
            ToggleUI(false);
        }
    }
}