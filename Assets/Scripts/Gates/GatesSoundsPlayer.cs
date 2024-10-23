using Core;
using UnityEngine;

namespace Gates
{
    public class GatesSoundsPlayer : MonoBehaviour
    {
        [SerializeField] private GatesSwitcher gatesSwitcher;
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip closeSound;

        private void OnEnable()
        {
            gatesSwitcher.GatesSwitched += PlaySound;
        }

        private void OnDisable()
        {
            gatesSwitcher.GatesSwitched -= PlaySound;
        }

        private void PlaySound(bool isOpen)
        {
            if (isOpen)
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