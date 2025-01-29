using System;
using Characters;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerDialogueInteraction : MonoBehaviour
    {
        // [SerializeField] private DialogueManager _dialogueManager;
        
        private Character _currentCharacter;

        private void Update()
        {
            if (_currentCharacter == null) return;

            if (Input.GetKeyDown(KeyCode.F))
            {
                // bool started = _currentCharacter.TryStartDialogue();
                // Debug.Log(started);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Entered");
            
            if (other.TryGetComponent(out Character character))
            {
                _currentCharacter = character;
                // _currentCharacter.TriggerInteraction();
                Debug.Log("Character entered");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("Exited");
            
            if (other.TryGetComponent(out Character character) && character == _currentCharacter)
            {
                // _currentCharacter.EndInteraction();
                _currentCharacter = null;
                Debug.Log("Character exited");
            }
        }
    }
}