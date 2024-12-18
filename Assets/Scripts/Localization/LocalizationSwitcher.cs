using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Localization
{
    public class LocalizationSwitcher : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _languageDropdown;
        
        private Localizer _localizer;

        [Inject]
        public void Construct(Localizer localizer)
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

            string currentLanguage = _localizer.CurrentLanguage;
            
            switch (currentLanguage)
            {
                case "en":
                    _languageDropdown.value = 0;
                    break;
                case "ua":
                    _languageDropdown.value = 1;
                    break;
                case "ru":
                    _languageDropdown.value = 2;
                    break;
            }
        }
        
        private void OnLanguageSelected(int index)
        {
            switch (index)
            {
                case 0:
                    _localizer.SetLanguage("en");
                    break;
                case 1:
                    _localizer.SetLanguage("ua");
                    break;
                case 2:
                    _localizer.SetLanguage("ru");
                    break;
            }
        }
    }
}