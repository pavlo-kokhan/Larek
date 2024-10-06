using UnityEngine;

namespace Radio
{
    public class RadioVolumeUI : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private float _minRotationZ = 330f;
        private float _maxRotationZ = 30f;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnVolumeChanged(float newVolume, float minVolume, float maxVolume)
        {
            float rotationZ = Mathf.Lerp(_minRotationZ, _maxRotationZ, newVolume / maxVolume);
            _rectTransform.localRotation = Quaternion.Euler(0, 0, rotationZ);
        }
    }
}
