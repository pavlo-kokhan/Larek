using UnityEngine;

namespace Clock
{
    public class ClockAnimator : MonoBehaviour
    {
        [SerializeField] private Transform minutePointerTransform;
        [SerializeField] private Transform hourPointerTransform;

        private float _secondsRotationZ;
        private float _minuteRotationZ;
        private float _hourRotationZ;

        private void Start()
        {
            _minuteRotationZ = minutePointerTransform.localRotation.eulerAngles.z;
            _hourRotationZ = hourPointerTransform.localRotation.eulerAngles.z;
        }

        private void Update()
        {
            _minuteRotationZ = (_minuteRotationZ - Time.deltaTime * 6f * 10f) % 360;
            _hourRotationZ = (_hourRotationZ - Time.deltaTime * 0.5f * 10f) % 360;
            
            minutePointerTransform.localRotation = Quaternion.Euler(0, 0, _minuteRotationZ);
            hourPointerTransform.localRotation = Quaternion.Euler(0, 0, _hourRotationZ);
        }
    }
}