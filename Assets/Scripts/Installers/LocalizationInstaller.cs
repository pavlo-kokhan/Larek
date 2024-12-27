using System.IO;
using Localization;
using Zenject;

namespace Installers
{
    public class LocalizationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LocalizationLoader>()
                .ToSelf()
                .AsSingle()
                .WithArguments(Path.Combine("Localization", "Global"));

            Container.Bind<Localizer>()
                .ToSelf()
                .AsSingle();
        }
    }
}