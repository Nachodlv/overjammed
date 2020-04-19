using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Programmer.Necessities.Feelings
{
    public class Feeling : MonoBehaviour
    {
        [SerializeField] private AudioClip[] audios;
        [SerializeField] private Sprite sprite;
        [SerializeField] private KeyUI[] keys;

        public Sprite Sprite => sprite;

        public KeyUI[] Keys => keys;

        private Dictionary<KeyUI, Sprite> _keyDictionary;
        
        public AudioClip GetRandomAudioClip() => audios[Random.Range(0, audios.Length - 1)];
        
    }
}