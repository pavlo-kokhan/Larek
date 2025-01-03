using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Cursors
{
    public class CursorTextureChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Texture2D _cursorTexture;
        [Inject] private CursorView _cursorView;

        private void OnDisable()
        {
            _cursorView.SetDefaultCursorTexture();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _cursorView.SetCursorTexture(_cursorTexture);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _cursorView.SetDefaultCursorTexture();
        }
    }
}