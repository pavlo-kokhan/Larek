using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class HoverHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Color highlightColor = new Color(1f, 0.9f, 0.9f);
        [SerializeField] private Color defaultColor = Color.white;
        
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
            _spriteRenderer.color = isHighlighted ? highlightColor : defaultColor;
        }
    }
}