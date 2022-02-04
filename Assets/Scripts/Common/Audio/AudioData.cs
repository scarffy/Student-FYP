using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP.Data
{
    [CreateAssetMenu(fileName = "AudioData",menuName = "FYP/Data/AudioData",order = 0)]
    public class AudioData : ScriptableObject
    {
        /// <summary>
        /// List of audio clips
        /// </summary>
        public List<AudioClip> audioClipsList;

        public int GetRandom()
        {
            int temp = 0;
            if(audioClipsList.Count >= 1)
            {
                temp = Random.Range(0, audioClipsList.Count -1);
            }

            return temp;
        }

        public AudioClip GetAudioClip()
        {
            int temp = 0;
            AudioClip clip;
            if (audioClipsList.Count >= 1)
            {
                temp = Random.Range(0, audioClipsList.Count - 1);
            }
            clip = audioClipsList[temp];

            return clip;
        }
    }
}