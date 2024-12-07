using UnityEngine;

namespace Gates.Animations
{
    public class GatesAnimator : MonoBehaviour
    {
        private readonly int Opened = Animator.StringToHash(nameof(Opened));
        
        [SerializeField] private Animator animator;

        public void SetOpened(bool status)
        {
            animator.SetBool(Opened, status);
        }
    }
}