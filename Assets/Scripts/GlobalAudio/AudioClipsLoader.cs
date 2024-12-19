using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class AudioClipsLoader : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _audioClips;

        private void Start()
        {
            if (_audioClips.Count <= 0) return;
            
            foreach (var song in _audioClips)
            {
                song.LoadAudioData();
            }
        }
    }
}