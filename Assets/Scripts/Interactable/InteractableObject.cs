using UnityEngine;

namespace Interactable
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField] protected GameObject interactionPanel;
        
        protected SpriteRenderer _spriteRenderer;
        protected PolygonCollider2D _collider;

        private Color _initialColor;
        private readonly Color _interactingColor = new Color(1f, 0.9f, 0.9f);

        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<PolygonCollider2D>();

            _initialColor = _spriteRenderer.color;
        }
        
        protected virtual void Update()
        {
            if (IsMouseOver())
            {
                _spriteRenderer.color = _interactingColor;
                
                if (Input.GetMouseButtonDown(0))
                {
                    interactionPanel?.SetActive(true);
                }
            }
            else
            {
                _spriteRenderer.color = Color.white;
            }
        }
        
        protected virtual bool IsMouseOver()
        {
            if (Camera.main != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
                return _collider.OverlapPoint(mousePosition);
            }

            return false; // todo
        }
    }
}