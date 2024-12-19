using UnityEngine;

namespace Player
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
