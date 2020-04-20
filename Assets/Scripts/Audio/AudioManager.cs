using System;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using Utils;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager: MonoBehaviour
    {
        [SerializeField] private int audioSourceQuantity;
        [SerializeField] private AudioSourcePooleable audioSourcePrefab;
        [SerializeField] private AudioClip mainMusic;
        [SerializeField] private float fadeTime = 2f;
        
        public static AudioManager Instance;
        public bool Muted { get; private set; }
        public bool SoundEffectsMuted { get; set; }

        private AudioSource _audioSource;
        private ObjectPooler<AudioSourcePooleable> _pooler;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            
            PoolAudioSources();
        }

        private void Start()
        {
            _audioSource.clip = mainMusic;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        public void PlaySound(AudioClip clip, float volume = 1)
        {
            if (Muted || SoundEffectsMuted) return;
            var audioSource = _pooler.GetNextObject();
            audioSource.SetClip(clip);
            audioSource.SetVolume(volume);
            audioSource.StartClip();
        }

        public void PlaySoundWithFade(AudioClip clip, float volume)
        {
            if (Muted || SoundEffectsMuted) return;
            var audioSource = _pooler.GetNextObject();
            audioSource.SetClip(clip);
            StartCoroutine(AudioFades.FadeIn(audioSource.AudioSource, fadeTime, volume));
        } 
        
        public void PlaySound(AudioClip clip)
        {
            PlaySound(clip, 1);
        }

        public void Mute()
        {
            _audioSource.Pause();
            Muted = true;
        }

        public void UnMute()
        {
            _audioSource.Play();
            Muted = false;
        }
        
        public void ChangeClip(AudioClip clip)
        {
            _audioSource.clip = clip;
            mainMusic = clip;
            if(!Muted) _audioSource.Play();
        }

        public void FadeOutClip()
        {
            StartCoroutine(AudioFades.FadeOut(_audioSource, fadeTime));
        }

        public void FadeOutClip(float velocity)
        {
            StartCoroutine(AudioFades.FadeOut(_audioSource, velocity));
        }

        public void PoolAudioSources()
        {
            _pooler = new ObjectPooler<AudioSourcePooleable>();
            _pooler.InstantiateObjects(audioSourceQuantity, audioSourcePrefab, "Audio Sources");
        }
    }
}