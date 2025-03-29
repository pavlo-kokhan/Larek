using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Localization
{
    [CreateAssetMenu(fileName = "LocalizationConfig", menuName = "Configs/Localization/LocalizationConfig")]
    public class LocalizationConfig : ScriptableObject
    {
        [SerializeField] [SerializedDictionary]
        private SerializedDictionary<LanguageType, AssetReference> _assetReferences = new();
        
        public IReadOnlyDictionary<LanguageType, AssetReference> AssetReferences => _assetReferences;
    }
}