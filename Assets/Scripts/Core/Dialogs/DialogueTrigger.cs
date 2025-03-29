using Characters;
using UnityEngine;
using Zenject;

namespace Core.Dialogs
{
    [RequireComponent(typeof(Collider2D))]
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject _startDialogueIndicator;
        
        private DialogueService _dialogueService;

        private Character _character;
        private bool _canStartDialogue;

        [Inject]
        private void Construct(DialogueService dialogueService)
        {
            _dialogueService = dialogueService;
        }
        
        // private void Update()
        // {
        //     if (!_canStartDialogue) return;
        //
        //     if (Input.GetKeyDown(KeyCode.F))
        //     {
        //         _startDialogueIndicator.SetActive(false);
        //         _dialogueService.StartDialogue(_character);
        //         _canStartDialogue = false;
        //     }
        // }
        //
        // private void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (other.TryGetComponent(out Character character))
        //     {
        //         _character = character;
        //         _startDialogueIndicator.SetActive(true);
        //         _canStartDialogue = true;
        //     }
        // }
        //
        // private void OnTriggerExit2D(Collider2D other)
        // {
        //     if (other.TryGetComponent(out Character character))
        //     {
        //         _character = null;
        //         //
        //     }
        // }
    }
}