using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class HoverHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Color _highlightColor = new (1f, 0.9f, 0.9f);
        [SerializeField] private Color _defaultColor = Color.white;
        
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            HighlightObject(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HighlightObject(false);
        }
        
        private void HighlightObject(bool isHighlighted)
        {
            _spriteRenderer.color = isHighlighted ? _highlightColor : _defaultColor;
        }
    }
}