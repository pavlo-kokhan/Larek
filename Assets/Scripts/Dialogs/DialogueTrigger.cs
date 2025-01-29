using Characters;
using UnityEngine;
using Zenject;

namespace Dialogs
{
    [RequireComponent(typeof(Collider2D))]
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject _startDialogueIndicator;
        
        [Inject] private DialogueService _dialogueService;

        private Character _character;
        private bool _canStartDialogue;

        private void Update()
        {
            if (!_canStartDialogue) return;

            if (Input.GetKeyDown(KeyCode.F))
            {
                _startDialogueIndicator.SetActive(false);
                _dialogueService.StartDialogue(_character);
                _canStartDialogue = false;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Character character))
            {
                _character = character;
                _startDialogueIndicator.SetActive(true);
                _canStartDialogue = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Character character))
            {
                _character = null;
            }
        }
    }
}