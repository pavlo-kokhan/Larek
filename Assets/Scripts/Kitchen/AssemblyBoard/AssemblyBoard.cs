using Core;
using Core.Panels;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.AssemblyBoard
{
    [RequireComponent(typeof(Collider2D))]
    public class AssemblyBoard : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject _sculptingPanelPrefab;
        [SerializeField] private GameObject _assemblyPanelPrefab;

        private InteractivePanelsFactory _panelsFactory;
        private InteractivePanelsRegistrator _panelsRegistrator;
        
        private GameObject _sculptingPanelInstance;
        private GameObject _assemblyPanelInstance;
        
        [Inject]
        private void Construct(InteractivePanelsFactory panelsFactory, InteractivePanelsRegistrator panelsRegistrator)
        {
            _panelsFactory = panelsFactory;
            _panelsRegistrator = panelsRegistrator;
        }
        
        private void Start()
        {
            CreateSculptingPanel();
            CreateAssemblyPanel();            
            OnPanelLoaded();
        }

        private void CreateSculptingPanel()
        {
            _sculptingPanelInstance = _panelsFactory.Create(_sculptingPanelPrefab, ContainerType.MiddleCenter);
            _sculptingPanelInstance.SetActive(false);
            _panelsRegistrator.RegisterPanel(_sculptingPanelInstance);
        }

        private void CreateAssemblyPanel()
        {
            _assemblyPanelInstance = _panelsFactory.Create(_assemblyPanelPrefab, ContainerType.Stretch);
            _assemblyPanelInstance.SetActive(false);
            _panelsRegistrator.RegisterPanel(_assemblyPanelInstance);
        }

        private void OnPanelLoaded()
        {
            
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftButtonClicked();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightButtonClicked();
            }
        }

        private void OnLeftButtonClicked()
        {
            _panelsRegistrator.ManuallyOpenPanel(_sculptingPanelInstance);
        }

        private void OnRightButtonClicked()
        {
            _panelsRegistrator.ManuallyOpenPanel(_assemblyPanelInstance);
        }
    }
}