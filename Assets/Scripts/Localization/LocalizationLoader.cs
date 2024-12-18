using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Localization
{
    public class LocalizationLoader
    {
        private readonly string _path;

        public LocalizationLoader(string path)
        {
            _path = path;
        }
        
        public Dictionary<string, string> LoadLocalization(string language)
        {
            string fullPath = Path.Combine(_path, $"{language}.json");

            if (!File.Exists(fullPath))
            {
                Debug.LogError($"Localization file not found: {fullPath}");
                return new Dictionary<string, string>();
            }

            string json = File.ReadAllText(fullPath);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}