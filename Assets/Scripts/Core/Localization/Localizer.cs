using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Localization
{
    public class Localizer
    {
        private const string LanguagePreferenceKey = "SelectedLanguage";
        private const string EnglishKey = "en";
        private const string UkrainianKey = "ua";
        private const string RussianKey = "ru";
        
        private readonly LocalizationProvider _localizationProvider;
        
        private readonly HashSet<LocalizedText> _localizedTextComponents = new();
        
        private Dictionary<string, string> _currentLanguageDictionary = new();

        private bool _loadedFirstLocalization;
     
        public string CurrentLanguageKey => PlayerPrefs.GetString(LanguagePreferenceKey, EnglishKey);
        public LanguageType CurrentLanguageType => GetLanguageType(CurrentLanguageKey);
        
        public Localizer(LocalizationProvider localizationProvider)
        {
            _localizationProvider = localizationProvider;
            SetLanguage(CurrentLanguageType);
            _loadedFirstLocalization = true;
        }
        
        public async void SetLanguage(LanguageType language)
        {
            _currentLanguageDictionary = await _localizationProvider.Load(language);
            _localizationProvider.Unload();

            var newKey = GetLanguageKey(language);
            
            PlayerPrefs.SetString(LanguagePreferenceKey, newKey);
            PlayerPrefs.Save();
            
            UpdateAllLocalizedTextComponents();
        }

        public string GetLocalizedText(string textKey)
        {
            if (_loadedFirstLocalization)
            {
                return _currentLanguageDictionary.GetValueOrDefault(textKey);   
            }

            return null;
        }
        
        private string GetLanguageKey(LanguageType language)
        {
            return language switch
            {
                LanguageType.English => EnglishKey,
                LanguageType.Ukrainian => UkrainianKey,
                LanguageType.Russian => RussianKey,
                _ => throw new InvalidOperationException($"Key of language type {language} is not found.")
            };
        }
        
        private LanguageType GetLanguageType(string languageKey)
        {
            return languageKey switch
            {
                EnglishKey => LanguageType.English,
                UkrainianKey => LanguageType.Ukrainian,
                RussianKey => LanguageType.Russian,
                _ => throw new InvalidOperationException($"Language type of language key {languageKey} is not found.")
            };
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