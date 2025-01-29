using UnityEngine;

namespace Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField] private float _speed = 5f;
        
        private Transform _spawnPoint;
        private Transform _interactionPoint;
        private Transform _leavePoint;
        
        private Vector3 _targetPosition;
        private bool _isMoving;

        private void Update()
        {
            if (_isMoving)
            {
                MoveTowardsTarget();
            }
        }

        public void Initialize(Transform spawnPoint, Transform interactionPoint, Transform leavePoint)
        {
            _spawnPoint = spawnPoint;
            _interactionPoint = interactionPoint;
            _leavePoint = leavePoint;
            transform.position = _spawnPoint.position;
        }

        public void MoveToInteractionPoint()
        {
            _targetPosition = _interactionPoint.position;
            StartMovement();
        }

        public void MoveToLeavePoint()
        {
            _targetPosition = _leavePoint.position;
            StartMovement();
        }

        private void StartMovement()
        {
            _isMoving = true;
            _animator.SetMoving(true);
        }

        private void StopMovement()
        {
            _isMoving = false;
            _animator.SetMoving(false);
        }

        private void MoveTowardsTarget()
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                _targetPosition, 
                _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
            {
                StopMovement();
            }
        }
    }
}