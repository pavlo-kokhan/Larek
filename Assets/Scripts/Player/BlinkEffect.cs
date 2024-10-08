using System.Collections;
using UnityEngine;

namespace Player
{
    public class BlinkEffect : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator animator;
        [SerializeField] private float minBlinkTime;
        [SerializeField] private float maxBlinkTime;

        private bool _isLooping = true;

        private void Start()
        {
            StartCoroutine(Blinking());
        }

        private IEnumerator Blinking()
        {
            while (_isLooping)
            {
                yield return new WaitForSeconds(Random.Range(minBlinkTime, maxBlinkTime));
                animator.SetBlink();
            }
        }
    }
}