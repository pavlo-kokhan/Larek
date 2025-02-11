using GlobalAudio;
using UnityEngine;
using Zenject;

namespace FrontSide.Shelves
{
    public class ShelfAudioPlayer : MonoBehaviour
    {
        [SerializeField] private ShelfOpener _shelfOpener;
        [SerializeField] private ShelfCloser _shelfCloser;
        [SerializeField] private AudioClip _openSound;
        [SerializeField] private AudioClip _closeSound;
        
        [Inject] private GlobalAudioPlayer _globalAudioPlayer;

        private void OnEnable()
        {
            _shelfOpener.ShelfOpened += PlayOpenSound;
            _shelfCloser.ShelfClosed += PlayCloseSound;
        }

        private void OnDisable() 
        {
            _shelfOpener.ShelfOpened -= PlayOpenSound;
            _shelfCloser.ShelfClosed -= PlayCloseSound;
        }

        private void PlayOpenSound()
        {
            _globalAudioPlayer.PlaySoundEffect(_openSound);
        }

        private void PlayCloseSound()
        {
            _globalAudioPlayer.PlaySoundEffect(_closeSound);
        }
    }
}