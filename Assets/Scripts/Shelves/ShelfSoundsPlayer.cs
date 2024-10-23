using Core;
using UnityEngine;

namespace Shelves
{
    public class ShelfSoundsPlayer : MonoBehaviour
    {
        [SerializeField] private ShelfOpener shelfOpener;
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip closeSound;

        private void OnEnable()
        {
            shelfOpener.ShelfSwitched += PlayOpenSound;
        }

        private void OnDisable() 
        {
            shelfOpener.ShelfSwitched -= PlayOpenSound;
        }

        private void PlayOpenSound(bool isOpened)
        {
            if (isOpened)
            {
                AudioManager.Instance.PlaySfx(openSound);
            }
            else
            {
                AudioManager.Instance.PlaySfx(closeSound);
            }
        }
    }
}