using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dialogs
{
    public class DialoguePanel : MonoBehaviour
    {
        [SerializeField] private GameObject _optionPrefab;
        [SerializeField] private Transform _optionsParent;

        private List<Option> _currentOptions = new();

        private void OnDisable()
        {
            ClearOptions();
        }

        public IEnumerable<Option> UpdateDialogue(List<Ink.Runtime.Choice> choices)
        {
            ClearOptions();

            for (int i = 0; i < choices.Count; i++)
            {
                var optionInstance = Instantiate(_optionPrefab, _optionsParent);
                var optionComponent = optionInstance.GetComponent<Option>();

                optionComponent.Initialize(i, choices[i].text);
                _currentOptions.Add(optionComponent);
            }
            
            return _currentOptions;
        }
        
        private void ClearOptions()
        {
            foreach (var option in _currentOptions)
            {
                Destroy(option.gameObject);
            }
            
            _currentOptions.Clear();
        }
    }
}