using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Collider2D))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private TextAsset _inkJson;
        [SerializeField] private GameObject _phrasePanel;
        [SerializeField] private TMP_Text _phrasesText;

        public string Name => _name;
        public TextAsset InkJson => _inkJson;

        private void Start()
        {
            DeactivatePhrasesPanel();
        }

        public void ActivatePhrasesPanel()
        {
            _phrasePanel.SetActive(true);
        }

        public void DeactivatePhrasesPanel()
        {
            _phrasePanel.SetActive(false);
        }
        
        public void UpdatePhrase(string phrase)
        {
            StartCoroutine(TypeNewPhraseCoroutine(phrase));
        }
        
        private IEnumerator TypeNewPhraseCoroutine(string phrase)
        {
            _phrasesText.text = string.Empty;
            
            foreach (var character in phrase)
            {
                _phrasesText.text += character;
                yield return new WaitForSeconds(0.05f);
            }

            yield return null;
        }
    }
}