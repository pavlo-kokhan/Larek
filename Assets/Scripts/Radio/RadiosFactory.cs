using System.Linq;
using Core;
using UnityEngine;
using Zenject;

namespace Radio
{
    // public class RadiosFactory : IFactory<GameObject>
    // {
    //     private readonly DiContainer _container;
    //     private readonly GameObject _radioPrefab;
    //     private readonly RadioPanelsFactory _radioPanelsFactory;
    //     private readonly Transform _radioParent;
    //
    //     public RadiosFactory(DiContainer container, GameObject radioPrefab, RadioPanelsFactory radioPanelsFactory, Transform radioParent)
    //     {
    //         _container = container;
    //         _radioPrefab = radioPrefab;
    //         _radioPanelsFactory = radioPanelsFactory;
    //         _radioParent = radioParent;
    //     }
    //     
    //     public GameObject Create()
    //     {
    //         var radioPanel = _radioPanelsFactory.Create();
    //         radioPanel.SetActive(false);
    //
    //         var sliderInputs = radioPanel
    //             .GetComponentsInChildren<RadioSliderInput>(true);
    //
    //         var volumeSliderInput = sliderInputs.FirstOrDefault(c => c.Type == RadioSliderType.Volume);
    //         var channelSliderInput = sliderInputs.FirstOrDefault(c => c.Type == RadioSliderType.Channel);
    //         
    //         var radioInstance = _container.InstantiatePrefab(_radioPrefab, _radioParent);
    //         
    //         var clickableObject = radioInstance.GetComponent<ClickableObjectWithUI>();
    //         clickableObject.Initialize(radioPanel);
    //         
    //         var radioPlayer = radioInstance.GetComponent<RadioPlayer>();
    //         radioPlayer.Initialize(volumeSliderInput, channelSliderInput);
    //         
    //         return radioInstance;
    //     }
    // }
}