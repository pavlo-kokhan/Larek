using UnityEngine;

namespace Radio
{
    public class RadioPanel : MonoBehaviour
    {
        [SerializeField] private VolumeInputController volumeInput;
        [SerializeField] private ChannelInputController channelInput;
        
        [SerializeField] private RadioVolumeUI volumeUI;
        [SerializeField] private RadioChannelUI channelUI;
        
        [SerializeField] private RadioVolumeSongs volumeSongs;
        [SerializeField] private RadioChannelSongs channelSongs;

        private void Start()
        {
            volumeInput.VolumeChangedUI += volumeUI.OnVolumeChanged;
            channelInput.ChannelChangedUI += channelUI.OnChannelChanged;
            
            volumeInput.VolumeChangedSong += volumeSongs.OnVolumeChanged;
            channelInput.ChannelChangedSong += channelSongs.OnChannelChanged;
        }
    }
}