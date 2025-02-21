using UnityEngine;
using UnityEngine.EventSystems;

namespace Kitchen.Hotplate
{
    [RequireComponent(typeof(Collider2D))]
    public class Hotplate : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}