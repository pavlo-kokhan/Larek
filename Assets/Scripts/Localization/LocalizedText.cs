using TMPro;
using UnityEngine;
using Zenject;

namespace Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _localizedTextKey;

        private TextMeshProUGUI _textMeshPro;
        private Localizer _localizer;

        [Inject]
        public void Construct(Localizer localizer)
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
            if (_textMeshPro != null && _localizer != null)
            {
                _textMeshPro.text = _localizer.GetLocalizedText(_localizedTextKey);
            }
        }
    }
}