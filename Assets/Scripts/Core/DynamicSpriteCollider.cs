using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(SpriteRenderer), typeof(PolygonCollider2D))]
    public class DynamicSpriteCollider : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private PolygonCollider2D _polygonCollider;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _polygonCollider = GetComponent<PolygonCollider2D>();
        }

        public void SetSprite(Sprite newSprite)
        {
            _spriteRenderer.sprite = newSprite;
            UpdateCollider(newSprite);
        }
        
        private void UpdateCollider(Sprite newSprite)
        {
            _polygonCollider.pathCount = newSprite.GetPhysicsShapeCount();
        
            for (var i = 0; i < _polygonCollider.pathCount; i++)
            {
                var points = new List<Vector2>();
                
                newSprite.GetPhysicsShape(i, points);
                _polygonCollider.SetPath(i, points.ToArray());
            }
        }
    }
}