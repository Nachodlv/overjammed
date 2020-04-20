using System;
using System.Collections;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourcePooleable: Pooleable
    {
        private Func<IEnumerator> _playingSoundCoroutine;
        public Transform Transform { get; private set; }

        public AudioSource AudioSource { get; private set; }

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
            AudioSource.playOnAwake = false;
            AudioSource.loop = false;
            _playingSoundCoroutine = WaitClipToStop;
            Transform = transform;
        }

        public void SetClip(AudioClip clip)
        {
            AudioSource.clip = clip;
        }

        public void SetVolume(float volume)
        {
            AudioSource.volume = volume;
        }
        
        public void StartClip()
        {
            AudioSource.Play();
            StartCoroutine(_playingSoundCoroutine());
        }

        private IEnumerator WaitClipToStop()
        {
            var clipLength = AudioSource.clip.length;
            var now = Time.time;
            while (Time.time - now < clipLength)
                yield return null;
            Deactivate();
        }
    }
}