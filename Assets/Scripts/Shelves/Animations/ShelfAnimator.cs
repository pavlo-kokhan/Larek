using UnityEngine;

namespace Shelves.Animations
{
    public class ShelfAnimator : MonoBehaviour
    {
        private readonly int Opened = Animator.StringToHash(nameof(Opened));
        
        [SerializeField] private Animator animator;
        
        public bool IsOpened => animator.GetBool(Opened);

        public void Open()
        {
            animator.SetBool(Opened, true);
        }
        
        public void Close()
        {
            animator.SetBool(Opened, false);
        }
    }
}
