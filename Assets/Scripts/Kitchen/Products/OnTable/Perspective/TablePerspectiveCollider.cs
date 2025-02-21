using System;
using UnityEngine;

namespace Kitchen.Products.OnTable.Perspective
{
    [RequireComponent(typeof(Collider2D))]
    public class TablePerspectiveCollider : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private TablePerspectiveType _perspectiveType;
        
        [Header("Scale range bottom - x, top - y")]
        [SerializeField] private Vector2 _scaleRange;
        
        public Collider2D Collider => _collider;
        public Bounds Bounds => _collider.bounds;
        public TablePerspectiveType PerspectiveType => _perspectiveType;
        public Vector2 ScaleRange => _scaleRange;

        private void OnDrawGizmos()
        {
            if (_collider == null) return;

            Gizmos.color = Color.red;
            
            var collider = _collider as PolygonCollider2D;
    
            for (int i = 0; i < collider?.pathCount; i++)
            {
                var path = collider.GetPath(i);
                for (int j = 0; j < path.Length; j++)
                {
                    Vector3 currentPoint = transform.TransformPoint(path[j]);
                    Vector3 nextPoint = transform.TransformPoint(path[(j + 1) % path.Length]);
                    Gizmos.DrawLine(currentPoint, nextPoint);
                }
            }
        }
    }
}