using System.Linq;
using Core;
using UnityEngine;

namespace FrontSide.Radio
{
    [RequireComponent(typeof(RadioPlayer))]
    public class Radio : ClickableObjectWithUI
    {
        [SerializeField] private RadioPlayer _radioPlayer;
        
        protected override void OnPanelLoaded()
        {
            var sliderInputs = _interactivePanelInstance.GetComponentsInChildren<RadioSliderInput>();
            
            if (sliderInputs == null)
            {
                Debug.LogError($"{_interactivePanelPrefab.name} has no required {nameof(RadioSliderInput)} components");
                return;
            }
            
            var volumeInput = sliderInputs.FirstOrDefault(i => i.Type == RadioSliderType.Volume);
            var channelInput = sliderInputs.FirstOrDefault(i => i.Type == RadioSliderType.Channel);

            if (volumeInput == null || channelInput == null)
            {
                Debug.LogError($"{_interactivePanelPrefab.name} both or one of {nameof(RadioSliderInput)} components are missing");
                return;
            }
            
            _radioPlayer.Initialize(volumeInput, channelInput);
        }
    }
}