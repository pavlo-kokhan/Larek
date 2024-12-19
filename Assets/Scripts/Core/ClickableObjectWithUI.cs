using Panels;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class ClickableObjectWithUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject _interactivePanelPrefab;
        [Inject] private InteractivePanelsRegistrator _panelsRegistrator;
        [Inject] private InteractivePanelsFactory _panelsFactory;
        
        private GameObject _interactivePanelInstance;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftButtonClicked();
            }
        }

        private void OnLeftButtonClicked()
        {
            if (_interactivePanelInstance == null)
            {
                _interactivePanelInstance = _panelsFactory.Create(_interactivePanelPrefab);
            }
                
            _panelsRegistrator.RegisterPanel(_interactivePanelInstance);
        }
    }
}