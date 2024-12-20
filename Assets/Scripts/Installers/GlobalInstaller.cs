using Localization;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LocalizationLoader>()
                .ToSelf()
                .AsSingle()
                .WithArguments($"{Application.dataPath}/Resources/Localization/Static Texts");

            Container.Bind<Localizer>()
                .ToSelf()
                .AsSingle();
        }
    }
}