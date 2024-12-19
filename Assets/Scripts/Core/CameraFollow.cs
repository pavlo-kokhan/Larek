using UnityEngine;

namespace Core
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private Vector3 _initialPosition;

        private void Start()
        {
            _initialPosition = transform.position;
        }

        private void Update()
        {
            transform.position = new Vector3(_target.position.x, _target.position.y, _initialPosition.z);
        }
    }
}