using System.Collections.Generic;
using Core.RoomSidesSwitcherComponents;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Panels
{
    [RequireComponent(typeof(RectTransform))]
    public class ClosablePanel : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] public RoomType StartupRoom { get; private set; }
        [SerializeField] private List<RoomType> _allowedRooms = new();
        
        [Inject] private InteractivePanelsRegistrator _panelsRegistrator;
        
        private RoomType _currentRoom;
        private RectTransform _rectTransform;
        private Vector3 _startPosition;
        
        public List<RoomType> AllowedRooms => _allowedRooms;

        public RoomType CurrentRoom
        {
            get => _currentRoom;
            set
            {
                if (_allowedRooms.Contains(value) == false)
                {
                    Debug.LogWarning($"Panel can not be in room type: {value}");
                    return;
                }
                
                _currentRoom = value;
            }
        }

        private void OnValidate()
        {
            if (_allowedRooms.Contains(StartupRoom) == false)
            {
                _allowedRooms.Add(StartupRoom);
            }
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.anchoredPosition;
            CurrentRoom = StartupRoom;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                ClosePanel();
            }
        }
        
        public void ClosePanel()
        {
            _panelsRegistrator.ManuallyClosePanel(gameObject);
            _rectTransform.anchoredPosition = _startPosition;
        }
        
        public bool CanBeShownInRoom(RoomType roomType) => _allowedRooms.Contains(roomType);
    }
}