using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Localization
{
    public class LocaleSelector : MonoBehaviour
    {
        private bool _isActive;
        
        public void SetLocale(int localeId)
        {
            if (_isActive) return;
            
            StartCoroutine(SetLocaleCoroutine(localeId));
        }
        
        private IEnumerator SetLocaleCoroutine(int localeId)
        {
            _isActive = true;
            
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];
            
            _isActive = false;
        }
    }
}