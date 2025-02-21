using System;
using System.Collections;
using Core.Dialogs;
using TMPro;
using UnityEngine;
using Zenject;

namespace Characters
{
    public class CharacterPhrasePanel : MonoBehaviour
    {
        public event Action StartedTypingPhrase;
        public event Action EndedTypingPhrase;
        
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _phrasesText;
        [SerializeField] private float _characterPrintDelay = 0.05f;
        
        private void Start()
        {
            DeactivatePanel();
        }

        public void ActivatePanel()
        {
            _panel.SetActive(true);
        }

        public void DeactivatePanel()
        {
            _panel.SetActive(false);
        }

        public void UpdatePhrase(string phrase)
        {
            StartedTypingPhrase?.Invoke();
            StartCoroutine(TypeNewPhraseCoroutine(phrase));
        }
        
        private IEnumerator TypeNewPhraseCoroutine(string phrase)
        {
            _phrasesText.text = string.Empty;
            
            foreach (var character in phrase)
            {
                _phrasesText.text += character;
                yield return new WaitForSeconds(_characterPrintDelay);
            }

            EndedTypingPhrase?.Invoke();
            yield return null;
        }
    }
}