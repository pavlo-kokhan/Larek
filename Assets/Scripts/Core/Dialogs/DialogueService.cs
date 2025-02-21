using System;
using System.Collections.Generic;
using Characters;
using Ink.Runtime;
using UnityEngine;

namespace Core.Dialogs
{
    public class DialogueService
    {
        public event Action DialogueStarted;
        public event Action DialogueEnded;
        public event Action<string> PhraseUpdated;
        public event Action<IEnumerable<Choice>> ChoicesUpdated;
        
        private Story _story;
        private CharacterPhrasePanel _characterPhrasePanel;
        private readonly DialogueChoicesPanel _dialogueChoicesPanel;

        private List<DialogueChoice> _choiceComponents = new();
        
        private bool _isDialogueActive;

        public DialogueService(DialogueChoicesPanel dialogueChoicesPanel)
        {
            _dialogueChoicesPanel = dialogueChoicesPanel;
        }

        public void StartDialogue(CharacterPhrasePanel characterPhrasePanel, TextAsset inkJson)
        {
            if (_isDialogueActive) return;

            _story = new Story(inkJson.text);
            _characterPhrasePanel = characterPhrasePanel;
            
            _isDialogueActive = true;
            DialogueStarted?.Invoke();
            
            ContinueDialogue();
        }

        private void ContinueDialogue()
        {
            if (_story.canContinue)
            {
                _characterPhrasePanel.ActivatePanel();
                _characterPhrasePanel.UpdatePhrase(_story.Continue());
                _choiceComponents = _dialogueChoicesPanel.UpdateChoices(_characterPhrasePanel, _story.currentChoices);

                foreach (var choiceComponent in _choiceComponents)
                {
                    choiceComponent.ChoiceSelected += SelectChoice;
                }
                
                return;
            }

            EndDialogue();
        }

        private void SelectChoice(int choiceIndex)
        {
            _story.ChooseChoiceIndex(choiceIndex);
            ContinueDialogue();
        }

        private void EndDialogue()
        {
            _story = null;
            
            foreach (var choiceComponent in _choiceComponents)
            {
                choiceComponent.ChoiceSelected -= SelectChoice;
            }
            
            _isDialogueActive = false;
            
            DialogueEnded?.Invoke();
            
            Debug.Log("Dialogue ended. Some action happened.");
        }
    }
}