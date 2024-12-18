using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Localization
{
    public class Localizer
    {
        private const string LanguagePrefKey = "SelectedLanguage";

        private LocalizationLoader _localizationLoader;
        
        private readonly Dictionary<string, string> _enTexts;
        private readonly Dictionary<string, string> _uaTexts;
        private readonly Dictionary<string, string> _ruTexts;
        
        private Dictionary<string, string> _currentLanguageDictionary;
        
        private readonly HashSet<LocalizedText> _localizedTextComponents = new();
     
        public string CurrentLanguage => PlayerPrefs.GetString(LanguagePrefKey);
        
        public Localizer(LocalizationLoader localizationLoader)
        {
            _enTexts = localizationLoader.LoadLocalization("en");
            _uaTexts = localizationLoader.LoadLocalization("ua");
            _ruTexts = localizationLoader.LoadLocalization("ru");
            SetLanguage(CurrentLanguage);
        }
        
        public void SetLanguage(string language)
        {
            PlayerPrefs.SetString(LanguagePrefKey, language);
            PlayerPrefs.Save();
            
            _currentLanguageDictionary = SwitchLanguage(language);

            UpdateAllLocalizedTextComponents();
        }

        private Dictionary<string, string> SwitchLanguage(string language)
        {
            switch (language)
            {
                case "ua":
                    return _uaTexts;
                case "ru":
                    return _ruTexts;
                default:
                    return _enTexts;
            }
        }

        public string GetLocalizedText(string key)
        {
            return _currentLanguageDictionary.TryGetValue(key, out var value) 
                ? value 
                : $"[Missing: {key}]";
        }

        public void RegisterText(LocalizedText localizedText)
        {
            if (_localizedTextComponents.Add(localizedText))
            {
                localizedText.RefreshText();
            }
        }

        public void UnregisterText(LocalizedText localizedText)
        {
            _localizedTextComponents.Remove(localizedText);
        }

        private void UpdateAllLocalizedTextComponents()
        {
            foreach (var component in _localizedTextComponents)
            {
                component.RefreshText();
            }
        }
    }
}