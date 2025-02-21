using System.Collections.Generic;
using UnityEngine;

namespace Core.Localization
{
    public class Localizer
    {
        private const string LanguagePreferenceKey = "SelectedLanguage";
        
        private LocalizationLoader _localizationLoader;
        private readonly Dictionary<string, string> _enTexts;
        private readonly Dictionary<string, string> _uaTexts;
        private readonly Dictionary<string, string> _ruTexts;
        
        private Dictionary<string, string> _currentLanguageDictionary;
        
        private readonly HashSet<LocalizedText> _localizedTextComponents = new();
     
        public string CurrentLanguageKey => PlayerPrefs.GetString(LanguagePreferenceKey);
        public LanguageType CurrentLanguageType => LocalizationLoader.GetLanguageType(CurrentLanguageKey);
        
        public Localizer(LocalizationLoader localizationLoader)
        {
            _enTexts = localizationLoader.LoadLocalization(LanguageType.English);
            _uaTexts = localizationLoader.LoadLocalization(LanguageType.Ukrainian);
            _ruTexts = localizationLoader.LoadLocalization(LanguageType.Russian);
            SetLanguageByKey(CurrentLanguageKey);
        }
        
        public void SetLanguage(LanguageType language)
        {
            var languageKey = LocalizationLoader.GetLanguageKey(language);
            
            SetLanguageByKey(languageKey);
        }

        private void SetLanguageByKey(string languageKey)
        {
            PlayerPrefs.SetString(LanguagePreferenceKey, languageKey);
            PlayerPrefs.Save();
            
            _currentLanguageDictionary = GetCurrentLocalization(languageKey);

            UpdateAllLocalizedTextComponents();
        }

        private Dictionary<string, string> GetCurrentLocalization(string languageKey)
        {
            switch (languageKey)
            {
                case "ua":
                    return _uaTexts;
                case "ru":
                    return _ruTexts;
                default:
                    return _enTexts;
            }
        }

        public string GetLocalizedText(string textKey)
        {
            return _currentLanguageDictionary.TryGetValue(textKey, out var text) 
                ? text 
                : $"[Missing: {textKey}]";
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