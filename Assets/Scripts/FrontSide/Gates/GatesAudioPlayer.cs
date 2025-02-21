using Core.GlobalAudio;
using UnityEngine;
using Zenject;

namespace FrontSide.Gates
{
    public class GatesAudioPlayer : MonoBehaviour
    {
        [SerializeField] private GatesSwitcher _gatesSwitcher;
        [SerializeField] private AudioClip _openSound;
        [SerializeField] private AudioClip _closeSound;
        
        [Inject] private GlobalAudioPlayer _globalAudioPlayer;

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
            _globalAudioPlayer.PlaySoundEffect(clip);
        }
    }
}