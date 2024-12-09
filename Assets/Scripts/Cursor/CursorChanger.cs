using UnityEngine;
using UnityEngine.EventSystems;

namespace Cursor
{
    public class CursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CursorTextureMode _cursorTextureMode;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            CursorView.Instance.SetCursorTexture(_cursorTextureMode);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CursorView.Instance.SetDefaultCursorTexture();
        }
    }
}