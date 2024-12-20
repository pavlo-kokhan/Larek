using System.Linq;
using Core;
using UnityEngine;

namespace Radio
{
    [RequireComponent(typeof(RadioPlayer))]
    public class Radio : ClickableObjectWithUI
    {
        [SerializeField] private RadioPlayer _radioPlayer;
        
        protected override void OnPanelLoaded()
        {
            var sliderInputs = _interactivePanelInstance.GetComponentsInChildren<RadioSliderInput>();
            var volumeInput = sliderInputs.FirstOrDefault(i => i.Type == RadioSliderType.Volume);
            var channelInput = sliderInputs.FirstOrDefault(i => i.Type == RadioSliderType.Channel);

            if (volumeInput == null || channelInput == null)
            {
                Debug.LogError($"{_interactivePanelPrefab.name} has no required slider input components");
                return;
            }
            
            _radioPlayer.Initialize(volumeInput, channelInput);
        }
    }
}