using UnityEngine;

namespace FrontSide.Clock
{
    public class ClockAnimator : MonoBehaviour
    {
        [SerializeField] private Transform _minutePointerTransform;
        [SerializeField] private Transform _hourPointerTransform;

        private float _secondsRotationZ;
        private float _minuteRotationZ;
        private float _hourRotationZ;

        private void Start()
        {
            _minuteRotationZ = _minutePointerTransform.localRotation.eulerAngles.z;
            _hourRotationZ = _hourPointerTransform.localRotation.eulerAngles.z;
        }

        private void Update()
        {
            _minuteRotationZ = (_minuteRotationZ - Time.deltaTime * 6f * 10f) % 360;
            _hourRotationZ = (_hourRotationZ - Time.deltaTime * 0.5f * 10f) % 360;
            
            _minutePointerTransform.localRotation = Quaternion.Euler(0, 0, _minuteRotationZ);
            _hourPointerTransform.localRotation = Quaternion.Euler(0, 0, _hourRotationZ);
        }
    }
}