using Core;
using UnityEngine;

namespace Shelves.Sounds
{
    public class ShelfSoundsPlayer : MonoBehaviour
    {
        [SerializeField] private ShelfOpener _shelfOpener;
        [SerializeField] private ShelfCloser _shelfCloser;
        [SerializeField] private AudioClip _openSound;
        [SerializeField] private AudioClip _closeSound;

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
            AudioManager.Instance.PlaySfx(_openSound);
        }

        private void PlayCloseSound()
        {
            AudioManager.Instance.PlaySfx(_closeSound);
        }
    }
}