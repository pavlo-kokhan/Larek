using Characters;
using Ink.Runtime;
using UnityEngine;

namespace Dialogs
{
    public class DialogueService
    {
        private readonly GameObject _dialoguePanel;
        private DialoguePanel _dialoguePanelComponent;
        
        private Character _currentCharacter;
        private Story _currentStory;
        
        private bool _isDialogueActive;

        public DialogueService(GameObject dialoguePanel)
        {
            _dialoguePanel = dialoguePanel;
        }

        public void StartDialogue(Character character)
        {
            if (_isDialogueActive) return;

            _currentCharacter = character;
            _currentStory = new Story(_currentCharacter.InkJson.text);
            
            _isDialogueActive = true;

            _dialoguePanel.SetActive(true);
            _dialoguePanelComponent = _dialoguePanel.GetComponent<DialoguePanel>();
            
            _currentCharacter.ActivatePhrasesPanel();
            
            ContinueDialogue();
        }

        private void ContinueDialogue()
        {
            if (_currentStory.canContinue)
            {
                var characterPhrase = _currentStory.Continue();
                _currentCharacter.UpdatePhrase(characterPhrase);
                
                var playerChoices = _currentStory.currentChoices;
                var options = _dialoguePanelComponent.UpdateDialogue(playerChoices);

                foreach (var option in options)
                {
                    option.OptionChosen += OnOptionChosen;
                }
                
                return;
            }

            EndDialogue();
        }

        private void OnOptionChosen(int choiceIndex)
        {
            _currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueDialogue();
        }

        private void EndDialogue()
        {
            _dialoguePanel.SetActive(false);
            _currentCharacter.DeactivatePhrasesPanel();
            _currentCharacter = null;
            _isDialogueActive = false;
            
            Debug.Log("Dialogue ended. Some action happened.");
        }
    }
}