using Core.Panels;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class ClickableObjectWithUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected GameObject _interactivePanelPrefab;

        [Inject] private InteractivePanelsFactory _panelsFactory;
        [Inject] private InteractivePanelsRegistrator _panelsRegistrator;
        
        protected GameObject _interactivePanelInstance;
        
        protected void Start()
        {
            _interactivePanelInstance = _panelsFactory.Create(_interactivePanelPrefab);
            _interactivePanelInstance.SetActive(false);
            _panelsRegistrator.RegisterPanel(_interactivePanelInstance);
            
            OnPanelLoaded();
        }

        protected abstract void OnPanelLoaded();

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftButtonClicked();
            }
        }

        private void OnLeftButtonClicked()
        {
            if (_interactivePanelInstance == null) return;
            
            _panelsRegistrator.ManuallyOpenPanel(_interactivePanelInstance);
        }
    }
}