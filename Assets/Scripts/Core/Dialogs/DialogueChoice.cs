using System;
using Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Dialogs
{
    public class DialogueChoice : MonoBehaviour
    {
        public event Action<int> ChoiceSelected;
        
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;

        private CharacterPhrasePanel _characterPhrasePanel;
        private int _choiceIndex;
        private bool _isInitialized;

        public void Initialize(CharacterPhrasePanel characterPhrasePanel, int choiceIndex, string text)
        {
            if (_isInitialized) return;

            _characterPhrasePanel = characterPhrasePanel;
            
            _characterPhrasePanel.StartedTypingPhrase += RemoveClickEventListener;
            _characterPhrasePanel.EndedTypingPhrase += AddClickEventListener;
            
            _choiceIndex = choiceIndex;
            _text.text = text;
            
            _isInitialized = true;
        }

        private void OnDestroy()
        {
            _characterPhrasePanel.StartedTypingPhrase += RemoveClickEventListener;
            _characterPhrasePanel.EndedTypingPhrase += AddClickEventListener;
        }

        private void RemoveClickEventListener()
        {
            _button.onClick.RemoveListener(SelectChoice);
        }

        private void AddClickEventListener()
        {
            _button.onClick.AddListener(SelectChoice);
        }

        private void SelectChoice()
        {
            ChoiceSelected?.Invoke(_choiceIndex);
        }
    }
}