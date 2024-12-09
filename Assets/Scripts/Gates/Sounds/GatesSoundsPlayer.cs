using Core;
using UnityEngine;

namespace Gates.Sounds
{
    public class GatesSoundsPlayer : MonoBehaviour
    {
        [SerializeField] private GatesSwitcher _gatesSwitcher;
        [SerializeField] private AudioClip _openSound;
        [SerializeField] private AudioClip _closeSound;

        private void OnEnable()
        {
            _gatesSwitcher.GatesSwitched += PlaySound;
        }

        private void OnDisable()
        {
            _gatesSwitcher.GatesSwitched -= PlaySound;
        }

        private void PlaySound(bool isOpen)
        {
            var clip = isOpen ? _openSound : _closeSound;
            AudioManager.Instance.PlaySfx(clip);
        }
    }
}