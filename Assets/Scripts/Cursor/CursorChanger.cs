using UnityEngine;
using UnityEngine.EventSystems;

namespace Cursor
{
    public class CursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CursorTextureMode cursorTextureMode;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            CursorView.Instance.SetCursorTexture(cursorTextureMode);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CursorView.Instance.SetDefaultCursorTexture();
        }
    }
}