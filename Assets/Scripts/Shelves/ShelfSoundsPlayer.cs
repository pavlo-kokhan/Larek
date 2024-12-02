using System;
using Core;
using UnityEngine;

namespace Shelves
{
    public class ShelfSoundsPlayer : MonoBehaviour
    {
        [SerializeField] private ShelfOpener shelfOpener;
        [SerializeField] private ShelfCloser shelfCloser;
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip closeSound;

        private void OnEnable()
        {
            shelfOpener.ShelfOpened += PlayOpenSound;
            shelfCloser.ShelfClosed += PlayCloseSound;
        }

        private void OnDisable() 
        {
            shelfOpener.ShelfOpened -= PlayOpenSound;
            shelfCloser.ShelfClosed -= PlayCloseSound;
        }

        private void PlayOpenSound()
        {
            AudioManager.Instance.PlaySfx(openSound);
        }

        private void PlayCloseSound()
        {
            AudioManager.Instance.PlaySfx(closeSound);
        }
    }
}