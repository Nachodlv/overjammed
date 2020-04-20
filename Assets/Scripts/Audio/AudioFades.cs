using System;
using System.Collections;
using UnityEngine;

namespace Audio
{
    public class AudioFades
    {
 
        public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime) {
            var startVolume = audioSource.volume;

            while (audioSource.volume > 0) {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

                yield return null;
            }

            audioSource.Stop ();
            audioSource.volume = startVolume;
        }
        
        
        public static IEnumerator FadeIn(AudioSource audioSource, float fadeTime, float maxVolume) {
            var startVolume = audioSource.volume;
            audioSource.volume = 0;
            audioSource.Play();

            while (Math.Abs(audioSource.volume - maxVolume) < 0.01f) {
                audioSource.volume += startVolume * Time.deltaTime / fadeTime;

                yield return null;
            }

            audioSource.volume = 1;
        }
        
 
    }
}