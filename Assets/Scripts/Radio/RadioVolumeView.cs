using UnityEngine;

namespace Radio
{
    public class RadioVolumeView : MonoBehaviour
    {
        [SerializeField] private RadioVolumeInput volumeInput;
        
        private RectTransform _rectTransform;
        private float _minRotationZ = 330f;
        private float _maxRotationZ = 30f;

        private void OnDisable()
        {
            volumeInput.VolumeChanged -= OnVolumeChanged;
        }

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            volumeInput.VolumeChanged += OnVolumeChanged;
        }

        private void OnVolumeChanged(float newVolume, float minVolume, float maxVolume)
        {
            float rotationZ = Mathf.Lerp(_minRotationZ, _maxRotationZ, newVolume / maxVolume);
            _rectTransform.localRotation = Quaternion.Euler(0, 0, rotationZ);
        }
    }
}
