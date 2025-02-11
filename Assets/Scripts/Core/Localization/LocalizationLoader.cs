using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Localization
{
    public class LocalizationLoader
    {
        private readonly string _path;

        public LocalizationLoader(string path)
        {
            _path = path;
        }
        
        public Dictionary<string, string> LoadLocalization(LanguageType language)
        {
            var languageFileName = GetLanguageKey(language);
            var fullPath = $"{_path}/{languageFileName}";
            var file = Resources.Load<TextAsset>(fullPath);
            
            if (file == null)
            {
                Debug.LogError($"Localization file not found: {fullPath}");
                return new Dictionary<string, string>();
            }

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(file.text);
        }

        public static string GetLanguageKey(LanguageType language)
        {
            switch (language)
            {
                case LanguageType.Ukrainian:
                    return "ua";
                case LanguageType.Russian:
                    return "ru";
                default:
                    return "en";
            }
        }
    }
}