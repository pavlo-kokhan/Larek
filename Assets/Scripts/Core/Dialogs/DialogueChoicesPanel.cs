using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using UnityEngine;

namespace Core.Dialogs
{
    public class DialogueChoicesPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _choicePrefab;
        [SerializeField] private Transform _choicesParent;

        private readonly List<DialogueChoice> _currentChoices = new();
        
        public List<DialogueChoice> UpdateChoices(CharacterPhrasePanel characterPhrasePanel, List<Ink.Runtime.Choice> choices)
        {
            ClearOptions();

            for (int i = 0; i < choices.Count; i++)
            {
                var choiceInstance = Instantiate(_choicePrefab, _choicesParent);
                var choiceComponent = choiceInstance.GetComponent<DialogueChoice>();

                choiceComponent.Initialize(characterPhrasePanel, i, choices[i].text);
                _currentChoices.Add(choiceComponent);
            }
            
            return _currentChoices;
        }
        
        private void ClearOptions()
        {
            foreach (var choice in _currentChoices)
            {
                Destroy(choice.gameObject);
            }
            
            _currentChoices.Clear();
        }
    }
}