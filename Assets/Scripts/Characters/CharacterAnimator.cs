using UnityEngine;

namespace Characters
{
    public class CharacterAnimator : MonoBehaviour
    {
        private static readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));
        
        [SerializeField] private Animator _animator;

        public void SetMoving(bool isMoving)
        {
            _animator.SetBool(IsMoving, isMoving);
        }
    }
}