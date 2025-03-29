using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _localizedTextKey;
        [SerializeField] private string _defaultText;
        
        private Localizer _localizer;
        
        private TextMeshProUGUI _textMeshPro;

        [Inject]
        private void Construct(Localizer localizer)
        {
            _localizer = localizer;
        }
        
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
            if (_textMeshPro is not null && _localizer is not null)
            {
                var text = _localizer.GetLocalizedText(_localizedTextKey);

                if (text is null)
                {
                    _textMeshPro.text = _defaultText;
                    return;
                }
                
                _textMeshPro.text = text;
            }
        }
    }
}