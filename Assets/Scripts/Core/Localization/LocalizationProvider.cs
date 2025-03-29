using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Localization
{
    public class LocalizationLoader
    {
        private readonly LocalizationConfig _localizationConfig;

        private TextAsset _cachedTextAsset;

        public LocalizationLoader(LocalizationConfig localizationConfig)
        {
            _localizationConfig = localizationConfig;
        }
        
        public async Task<Dictionary<string, string>> LoadLocalization(LanguageType language)
        {
            var referencesDictionary = _localizationConfig.AssetReferences;
            
            if (!referencesDictionary.TryGetValue(language, out var assetReference))
            {
                Debug.LogError($"File reference for language: {language} is not found");
                return new Dictionary<string, string>();
            }
            
            try
            {
                var handle = Addressables.LoadAssetAsync<TextAsset>(assetReference);
                _cachedTextAsset = await handle.Task;
                
                if (_cachedTextAsset is null)
                {
                    Debug.LogError($"Localization file for {language} is missing.");
                    return new Dictionary<string, string>();
                }
                
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(_cachedTextAsset.text)
                       ?? new Dictionary<string, string>();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load localization for {language}: {e.Message}");
                return new Dictionary<string, string>();
            }
        }

        public void Unload()
        {
            
        }
    }
}