using UnityEngine;

namespace Shelves
{
    public class ShelfAnimator : MonoBehaviour
    {
        private static readonly int Opened = Animator.StringToHash(nameof(Opened));
        
        [SerializeField] private Animator _animator;
        
        public bool IsOpened => _animator.GetBool(Opened);

        public void Open()
        {
            _animator.SetBool(Opened, true);
        }
        
        public void Close()
        {
            _animator.SetBool(Opened, false);
        }
    }
}
