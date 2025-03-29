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

        private InteractivePanelsFactory _panelsFactory;
        private InteractivePanelsRegistrator _panelsRegistrator;
        
        protected GameObject _interactivePanelInstance;

        [Inject]
        private void Construct(InteractivePanelsFactory panelsFactory, InteractivePanelsRegistrator panelsRegistrator)
        {
            _panelsFactory = panelsFactory;
            _panelsRegistrator = panelsRegistrator;
        }
        
        protected void Start()
        {
            _interactivePanelInstance = _panelsFactory.Create(_interactivePanelPrefab, ContainerType.MiddleCenter);
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

        protected virtual void OnLeftButtonClicked()
        {
            if (_interactivePanelInstance is null) return;
            
            _panelsRegistrator.ManuallyOpenPanel(_interactivePanelInstance);
        }
    }
}