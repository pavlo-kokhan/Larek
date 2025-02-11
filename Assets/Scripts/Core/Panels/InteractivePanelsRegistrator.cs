using System;
using System.Collections.Generic;
using Core.RoomSidesSwitcherComponents;
using UnityEngine;

namespace Core.Panels
{
    public class InteractivePanelsRegistrator : IDisposable
    {
        private readonly RoomSidesSwitcher _roomSidesSwitcher;
        
        private readonly Dictionary<GameObject, List<RoomType>> _panelAllowedRoomsMapping = new();
        private readonly Dictionary<GameObject, ClosablePanel> _panelClosablePanelMapping = new();
        private readonly Dictionary<RoomType, List<GameObject>> _roomActivePanelsMapping = new();

        private RoomType _previousRoom;

        public InteractivePanelsRegistrator(RoomSidesSwitcher roomSidesSwitcher)
        {
            _roomSidesSwitcher = roomSidesSwitcher;
            
            foreach (RoomType roomType in Enum.GetValues(typeof(RoomType)))
            {
                _roomActivePanelsMapping[roomType] = new List<GameObject>();    
            }

            _previousRoom = RoomType.FrontSide;
            
            _roomSidesSwitcher.RoomSideSwitched += OnRoomSideSwitched;
        }
        
        public void Dispose()
        {
            _roomSidesSwitcher.RoomSideSwitched -= OnRoomSideSwitched;
        }

        private void OnRoomSideSwitched(Collider2D collider, Vector3 center, RoomType newRoom)
        {
            var panelsToMove = new List<GameObject>();
            var panelsToActivate = new List<GameObject>();
            var panelsToDeactivate = new List<GameObject>();
            
            foreach (var panel in _roomActivePanelsMapping[_previousRoom])
            {
                var closablePanel = _panelClosablePanelMapping[panel];

                if (closablePanel.CanBeShownInRoom(newRoom))
                {
                    panelsToMove.Add(panel);
                    panelsToActivate.Add(panel);
                }
                else
                {
                    panelsToDeactivate.Add(panel);
                }
            }
            
            MovePanels(panelsToMove, newRoom);
            ActivatePanels(panelsToActivate);
            DeactivatePanels(panelsToDeactivate);
            ActivatePanels(_roomActivePanelsMapping[newRoom]);            
            
            _previousRoom = newRoom;
        }
        
        public void RegisterPanel(GameObject panel)
        {
            if (_panelAllowedRoomsMapping.ContainsKey(panel) ||
                panel.TryGetComponent<ClosablePanel>(out var closablePanel) == false) return;
            
            _panelClosablePanelMapping[panel] = closablePanel;
            _panelAllowedRoomsMapping[panel] = new List<RoomType>(closablePanel.AllowedRooms);
        }

        public void ManuallyOpenPanel(GameObject panel)
        {
            if (_panelAllowedRoomsMapping.ContainsKey(panel))
            {
                var closablePanel = _panelClosablePanelMapping[panel];
                
                _roomActivePanelsMapping[closablePanel.StartupRoom].Add(panel);
                
                ActivatePanel(panel);
            }
        }
        
        public void ManuallyClosePanel(GameObject panel)
        {
            if (_panelAllowedRoomsMapping.ContainsKey(panel))
            {
                var closablePanel = _panelClosablePanelMapping[panel];
                
                _roomActivePanelsMapping[closablePanel.CurrentRoom].Remove(panel);
                _roomActivePanelsMapping[closablePanel.StartupRoom].Remove(panel);

                DeactivatePanel(panel);
            }
        }

        private void MovePanels(IEnumerable<GameObject> panels, RoomType newRoom)
        {
            foreach (var panel in panels)
            {
                var closablePanel = _panelClosablePanelMapping[panel];
                
                _roomActivePanelsMapping[closablePanel.CurrentRoom].Remove(panel);
                _roomActivePanelsMapping[newRoom].Add(panel);
                
                closablePanel.CurrentRoom = newRoom;
            }
        }
        
        private void ActivatePanels(IEnumerable<GameObject> panels)
        {
            foreach (var panel in panels)
            {
                ActivatePanel(panel);
            }
        }
        
        private void ActivatePanel(GameObject panel)
        {
            if (panel.activeInHierarchy == false) panel.SetActive(true);
        }

        private void DeactivatePanels(IEnumerable<GameObject> panels)
        {
            foreach (var panel in panels)
            {
                DeactivatePanel(panel);
            }
        }

        private void DeactivatePanel(GameObject panel)
        {
            if (panel.activeInHierarchy) panel.SetActive(false);
        }
    }
}