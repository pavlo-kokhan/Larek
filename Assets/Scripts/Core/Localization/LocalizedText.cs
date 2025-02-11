using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _localizedTextKey;
        
        [Inject] private Localizer _localizer;
        
        private TextMeshProUGUI _textMeshPro;
        
        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _localizer.RegisterText(this);
        }
        
        private void OnDisable()
        {
            _localizer.UnregisterText(this);
        }

        public void RefreshText()
        {
            if (_textMeshPro != null && _localizer != null)
            {
                _textMeshPro.text = _localizer.GetLocalizedText(_localizedTextKey);
            }
        }
    }
}