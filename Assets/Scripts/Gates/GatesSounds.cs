using Core;
using UnityEngine;

namespace Gates
{
    public class GatesSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip closeSound;

        public void PlaySound(bool isOpen)
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