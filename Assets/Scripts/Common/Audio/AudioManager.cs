using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FYP.Data;

namespace FYP {
    /// <summary>
    /// This class will get data from scriptable object as source of truth
    /// </summary>
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Audio Source")]
        public AudioSource backgroundSource;
        public AudioSource sfxSource;

        [Header("Background Music Clip")]
        public AudioData backgroundMusic;

        IEnumerator Start()
        {
            backgroundSource.clip = backgroundMusic.GetAudioClip();
            backgroundSource.Play();
            yield return PlayBackground();
        }

        /// <summary>
        /// Will loop until we stop it.
        /// Maybe while is better performant?
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayBackground()
        {
            yield return new WaitWhile(() => backgroundSource.isPlaying);

            backgroundSource.clip = backgroundMusic.GetAudioClip();
            backgroundSource.Play();
            yield return PlayBackground();
        }

        public void PlaySfx()
        {

        }
    }
}