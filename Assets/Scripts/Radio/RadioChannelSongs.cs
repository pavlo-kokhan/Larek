using Core;
using UnityEngine;

namespace Radio
{
    public class RadioChannelSongs : MonoBehaviour
    {
        public void OnChannelChanged(AudioClip clip)
        {
            AudioManager.Instance.PlayMusic(clip);
        }
    }
}