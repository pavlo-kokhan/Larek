using Core;
using UnityEngine;

namespace Radio
{
    public class RadioVolumeSongs : MonoBehaviour
    {
        public void OnVolumeChanged(float volume)
        {
            AudioManager.Instance.SetMusicVolume(volume);
        }
    }
}