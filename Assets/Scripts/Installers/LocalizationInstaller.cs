using Core.Localization;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LocalizationInstaller : MonoInstaller
    {
        [SerializeField] private LocalizationConfig _localizationConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<LocalizationProvider>()
                .AsSingle()
                .WithArguments(_localizationConfig);

            Container.Bind<Localizer>()
                .AsSingle();
        }
    }
}