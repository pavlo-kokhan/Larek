using UnityEngine;

namespace Core.Cursors
{
    public class CursorFollower : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Vector3 _offset;
        
        private void Update()
        {
            Vector2 mousePosition = Input.mousePosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.GetComponent<RectTransform>(),
                mousePosition,
                _canvas.worldCamera,
                out Vector2 localPoint);

            _rectTransform.localPosition = new Vector3(localPoint.x + _offset.x, localPoint.y + _offset.y, 0f);
        }
    }
}