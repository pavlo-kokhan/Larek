using UnityEngine;

namespace Player.Effects
{
    public class BlinkEffectAnimator : MonoBehaviour
    {
        private static readonly int Blink = Animator.StringToHash(nameof(Blink));

        [SerializeField] private Animator animator;

        public void SetBlink()
        {
            animator.SetTrigger(Blink);
        }
    }
}
