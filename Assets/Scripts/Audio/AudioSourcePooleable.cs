using System;
using System.Collections;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourcePooleable: Pooleable
    {
        private AudioSource _audioSource;
        private Func<IEnumerator> _playingSoundCoroutine;
        public Transform Transform { get; private set; }
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
            _playingSoundCoroutine = WaitClipToStop;
            Transform = transform;
        }

        public void SetClip(AudioClip clip)
        {
            _audioSource.clip = clip;
        }

        public void SetVolume(float volume)
        {
            _audioSource.volume = volume;
        }
        
        public void StartClip()
        {
            _audioSource.Play();
            StartCoroutine(_playingSoundCoroutine());
        }

        private IEnumerator WaitClipToStop()
        {
            var clipLength = _audioSource.clip.length;
            var now = Time.time;
            while (Time.time - now < clipLength)
                yield return null;
            Deactivate();
        }
    }
}