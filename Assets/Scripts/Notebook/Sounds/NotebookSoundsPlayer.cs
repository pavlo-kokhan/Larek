using Notebook.Input;
using UnityEngine;

namespace Notebook.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class NotebookSoundsPlayer : MonoBehaviour
    {
        [SerializeField] private NotebookInput notebookInput;
        [SerializeField] private AudioClip pageTurningSound;

        private AudioSource _audioSource;

        private void OnEnable()
        {
            notebookInput.PageTurned += OnPageTurned;
        }

        private void OnDisable()
        {
            notebookInput.PageTurned -= OnPageTurned;
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnPageTurned()
        {
            _audioSource.PlayOneShot(pageTurningSound);
        }
    }
}