using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Kitchen.Hatch
{
    public class Hatch : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private RoomSidesSwitcher _roomSidesSwitcher;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _roomSidesSwitcher.OnUndergroundSideSwitch();
            }
        }
    }
}