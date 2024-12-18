using System;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Characters
{
    public class DialogueManager : MonoBehaviour
    {
        public event Action DialogueEnded;

        [SerializeField] private GameObject _dialoguePanel;
        [SerializeField] private TextMeshPro _dialogueText;
        [SerializeField] private Transform _playerChoicesContainer;
        
        private GameObject _playerChoicePrefab;
        private Story _currentStory;
        private bool _isStoryRunning;

        private void Awake()
        {
            _playerChoicePrefab = Resources.Load<GameObject>("Prefabs/Player/Dialogs/Choice");
            _isStoryRunning = false;
            _dialoguePanel.SetActive(false);
        }

        public void StartDialogue(TextAsset inkJson)
        {
            _currentStory = new Story(inkJson.text);
            _isStoryRunning = true;
            _dialoguePanel.SetActive(true);
            ContinueStory();
        }

        private void DisplayPlayerChoices()
        {
            ClearPlayerChoices();

            foreach (Choice choice in _currentStory.currentChoices)
            {
                var choiceObject = Instantiate(_playerChoicePrefab, _playerChoicesContainer);
                var choiceText = choiceObject.GetComponentInChildren<TextMeshProUGUI>();
                choiceText.text = choice.text;
                
                choiceObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    MakeChoice(choice.text);
                });
            }
        }

        private void MakeChoice(string optionText)
        {
            _currentStory.ChoosePathString(optionText);
            ContinueStory();
        }
        
        private void ContinueStory()
        {
            if (!_currentStory.canContinue)
            {
                DisplayPlayerChoices();
                return;
            }
            
            _dialogueText.text = _currentStory.Continue();
        }
        
        private void ClearPlayerChoices()
        {
            foreach (Transform child in _playerChoicesContainer)
            {
                Destroy(child.gameObject);
            }
        }
        
        private void EndDialogue()
        {
            _isStoryRunning = false;
            _dialoguePanel.SetActive(false);
            _dialogueText.text = string.Empty;
            DialogueEnded?.Invoke();
        }
    }
}