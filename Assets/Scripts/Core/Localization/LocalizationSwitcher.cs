using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.Localization
{
    public class LocalizationSwitcher : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _languageDropdown;
        
        private Localizer _localizer;

        [Inject]
        private void Construct(Localizer localizer)
        {
            _localizer = localizer;
        }

        private void Start()
        {
            SetupDropdown();
            _languageDropdown.onValueChanged.AddListener(OnLanguageSelected);
        }
        
        private void SetupDropdown()
        {
            _languageDropdown.options = new List<TMP_Dropdown.OptionData>
            {
                new ("English"),
                new ("Українська"),
                new ("Русский")
            };

            var currentLanguage = _localizer.CurrentLanguageType;
            
            switch (currentLanguage)
            {
                case LanguageType.English:
                    _languageDropdown.value = 0;
                    break;
                case LanguageType.Ukrainian:
                    _languageDropdown.value = 1;
                    break;
                case LanguageType.Russian:
                    _languageDropdown.value = 2;
                    break;
            }
        }
        
        private void OnLanguageSelected(int index)
        {
            switch (index)
            {
                case 0:
                    _localizer.SetLanguage(LanguageType.English);
                    break;
                case 1:
                    _localizer.SetLanguage(LanguageType.Ukrainian);
                    break;
                case 2:
                    _localizer.SetLanguage(LanguageType.Russian);
                    break;
            }
        }
    }
}