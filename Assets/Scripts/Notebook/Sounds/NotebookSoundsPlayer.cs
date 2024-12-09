using Notebook.Input;
using UnityEngine;

namespace Notebook.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class NotebookSoundsPlayer : MonoBehaviour
    {
        [SerializeField] private NotebookInput _notebookInput;
        [SerializeField] private AudioClip _pageTurningSound;

        private AudioSource _audioSource;

        private void OnEnable()
        {
            _notebookInput.PageTurned += OnPageTurned;
        }

        private void OnDisable()
        {
            _notebookInput.PageTurned -= OnPageTurned;
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnPageTurned()
        {
            _audioSource.PlayOneShot(_pageTurningSound);
        }
    }
}