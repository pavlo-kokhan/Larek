using UnityEngine;

namespace Core.Cursors
{
    public class CursorFollower : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _speed = 10f; // Швидкість руху
        [SerializeField] private float _damping = 5f; // Демпфер для інерції
        [SerializeField] private Vector2 _rotationConstraints = new(-15f, 15f); // обмеження на обертання
        [SerializeField] private float _rotationSpeed = 50f; // Швидкість обертання
        [SerializeField] private float _rotationDamping = 5f; // Гасіння коливань

        private RectTransform _canvasTransform;
        private Vector3 _velocity;
        private float _angularVelocity;
        private float _currentRotation;

        private void Awake()
        {
            _canvasTransform = _canvas.GetComponent<RectTransform>();
        }

        private void Update()
        {
            Vector2 mousePosition = Input.mousePosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasTransform,
                mousePosition,
                _canvas.worldCamera,
                out var localPoint);

            Vector3 targetPosition = new Vector3(localPoint.x + _offset.x, localPoint.y + _offset.y, 0f);

            // Реалізація згасаючих коливань (інерція руху)
            _velocity += (targetPosition - _rectTransform.localPosition) * (_speed * Time.deltaTime);
            _velocity *= Mathf.Exp(-_damping * Time.deltaTime);
            _rectTransform.localPosition += _velocity;

            // Обчислення кута нахилу на основі швидкості
            float targetRotation = Mathf.Clamp(-_velocity.x * _rotationSpeed, _rotationConstraints.x, _rotationConstraints.y); // Обмежуємо нахил

            // Згладжене обертання
            _angularVelocity += (targetRotation - _currentRotation) * (_rotationSpeed * Time.deltaTime);
            _angularVelocity *= Mathf.Exp(-_rotationDamping * Time.deltaTime);
            _currentRotation += _angularVelocity;

            _rectTransform.localRotation = Quaternion.Euler(0, 0, _currentRotation);
        }
    }
}