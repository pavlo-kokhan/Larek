using Cursors;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Kitchen.Refrigerator
{
    public class RefrigeratorSlotCursorTextureChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RefrigeratorSlot _refrigeratorSlot;
        [SerializeField] private Texture2D _textureTake;
        
        private CursorView _cursorView;

        [Inject]
        public void Construct(CursorView cursorView)
        {
            _cursorView = cursorView;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_refrigeratorSlot.CurrentProductsCount == 0) return;
            
            _cursorView.SetCursorTexture(_textureTake);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _cursorView.SetDefaultCursorTexture();
        }
    }
}