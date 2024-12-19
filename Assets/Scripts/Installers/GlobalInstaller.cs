using Localization;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private Vector2 _targetScreenAspectRatio;
        
        public override void InstallBindings()
        {
            Container.Bind<LocalizationLoader>()
                .ToSelf()
                .AsSingle()
                .WithArguments($"{Application.dataPath}/Resources/Localization/Static Texts");

            Container.Bind<Localizer>()
                .ToSelf()
                .AsSingle();
            
            // Container.Bind<ScreenAspectRatioApplier>()
            //     .ToSelf()
            //     .AsSingle()
            //     .WithArguments(_targetScreenAspectRatio, Camera.main);
        }
    }
}