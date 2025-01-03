using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Cursors
{
    public class CursorProductIconChanger : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Sprite _sprite;
        
        [Inject] private CursorView _cursorView;

        private bool _isSpriteSet;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (_isSpriteSet)
                {
                    _cursorView.ClearCursorProductIcon();
                    _isSpriteSet = false;
                    return;
                }
                
                _cursorView.SetCursorProductIcon(_sprite);
                _isSpriteSet = true;
            }
        }
    }
}