using UnityEngine;

namespace Shelves
{
    public class ShelfAnimator : MonoBehaviour
    {
        private readonly int Opened = Animator.StringToHash(nameof(Opened));
        
        [SerializeField] private Animator animator;

        public void SetOpened(bool status)
        {
            animator.SetBool(Opened, status);
        }
    }
}
