using UnityEngine;

namespace Core
{
    public class ClickableObject : MonoBehaviour
    {
        [SerializeField] protected GameObject interactionUI;
        [SerializeField] protected LayerMask clickableLayerMask;
        [SerializeField] protected Color highlightColor = new Color(1f, 0.9f, 0.9f);
        [SerializeField] protected Color defalutColor = Color.white;
        
        protected SpriteRenderer _spriteRenderer;
        protected bool _isActiveUI;

        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected virtual void Update()
        {
            if (_isActiveUI && Input.GetKeyUp(KeyCode.Escape))
            {
                ToggleUI(false);
            }
            
            if (IsMouseOver())
            {
                HighlightObject(true);

                if (Input.GetMouseButtonDown(0))
                {
                    ToggleUI(true);
                }
            }
            else
            {
                HighlightObject(false);
            }
        }
        
        protected virtual void ToggleUI(bool isActive)
        {
            interactionUI.SetActive(isActive);
            _isActiveUI = isActive;
            // SetLayersColliderState(!isActive);
        }
        
        protected virtual void SetLayersColliderState(bool state)
        {
            var colliders = FindObjectsOfType<PolygonCollider2D>();
            
            foreach (var obj in colliders)
            {
                if (state && (clickableLayerMask & (1 << obj.gameObject.layer)) != 0)
                {
                    obj.enabled = true;
                }
                else
                {
                    obj.enabled = false;
                }
            }
        }
        
        protected virtual bool IsMouseOver()
        {
            if (Camera.main != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(
                    mousePosition, 
                    Vector2.zero, 
                    0f, 
                    clickableLayerMask);

                return hit.collider != null && hit.collider.gameObject == gameObject;
            }

            return false;
        }
        
        protected virtual void HighlightObject(bool isHighlighted)
        {
            _spriteRenderer.color = isHighlighted ? highlightColor : defalutColor;
        }

        public void ClosePanel()
        {
            ToggleUI(false);
        }
    }
}