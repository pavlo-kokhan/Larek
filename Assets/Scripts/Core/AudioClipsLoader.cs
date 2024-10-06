using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class AudioClipsLoader : MonoBehaviour
    {
        [SerializeField] List<AudioClip> audioClips;

        private void Start()
        {
            if (audioClips.Count > 0)
            {
                foreach (var song in audioClips)
                {
                    song.LoadAudioData();
                }
            }
        }
    }
}