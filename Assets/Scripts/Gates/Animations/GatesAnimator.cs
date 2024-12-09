using UnityEngine;

namespace Gates.Animations
{
    public class GatesAnimator : MonoBehaviour
    {
        private static readonly int Opened = Animator.StringToHash(nameof(Opened));
        
        [SerializeField] private Animator _animator;

        public void SetOpened(bool status)
        {
            _animator.SetBool(Opened, status);
        }
    }
}