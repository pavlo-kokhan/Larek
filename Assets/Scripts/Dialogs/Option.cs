using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs
{
    public class Option : MonoBehaviour
    {
        public event Action<int> OptionChosen;
        
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;

        private int _choiceIndex;
        private bool _isInitialized;

        public void Initialize(int choiceIndex, string text)
        {
            if (_isInitialized) return;
            
            _choiceIndex = choiceIndex;
            _text.text = text;
            
            _isInitialized = true;
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(Choose);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Choose);
        }

        private void Choose()
        {
            OptionChosen?.Invoke(_choiceIndex);
        }
    }
}