using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Localization
{
    public class LocalizationProvider
    {
        private readonly LocalizationConfig _localizationConfig;

        private AsyncOperationHandle<TextAsset> _handle;

        public LocalizationProvider(LocalizationConfig localizationConfig)
        {
            _localizationConfig = localizationConfig;
        }
        
        public async Task<Dictionary<string, string>> Load(LanguageType language)
        {
            var referencesDictionary = _localizationConfig.AssetReferences;
            
            if (!referencesDictionary.TryGetValue(language, out var assetReference))
            {
                Debug.LogError($"File reference for language: {language} is not found");
                return new Dictionary<string, string>();
            }
            
            try
            {
                _handle = Addressables.LoadAssetAsync<TextAsset>(assetReference);
                var textAsset = await _handle.Task;
                
                if (textAsset is null)
                {
                    Debug.LogError($"Localization file for {language} is missing.");
                    return new Dictionary<string, string>();
                }
                
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(textAsset.text)
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
            if (_handle.IsValid())
            {
                Addressables.Release(_handle);
            }
        }
    }
}