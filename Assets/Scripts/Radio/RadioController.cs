using UnityEngine;

namespace Radio
{
    public class RadioController : MonoBehaviour
    {
        [SerializeField] private GameObject interactionPanel;
        
        private SpriteRenderer _spriteRenderer;
        private PolygonCollider2D _collider;
        private bool _isInteracting;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<PolygonCollider2D>();
        }

        private void Update()
        {
            if (IsMouseOver())
            {
                _spriteRenderer.color = new Color(1f, 0.9f, 0.9f); // todo
                
                if (Input.GetMouseButtonDown(0))
                {
                    interactionPanel.SetActive(true);
                }
            }
            else
            {
                _spriteRenderer.color = Color.white;
            }
        }
        
        private bool IsMouseOver()
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
