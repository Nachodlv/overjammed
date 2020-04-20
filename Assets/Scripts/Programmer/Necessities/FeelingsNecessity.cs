using System;
using Interactables;
using Player;
using Programmer.Necessities.Feelings;
using Sound;
using UI;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

namespace Programmer.Necessities
{
    public class FeelingsNecessity: Necessity
    {
        [SerializeField] private Feeling[] feelings;
        [SerializeField] private Interactable interactable;
        [SerializeField] private KeyCombinationDisplayer keyCombinationDisplayer;
        [SerializeField] private Light2D globalLight;
        [SerializeField] private float lightIntensityWhenTalking = 0.4f;
        
        private Feeling _currentFeeling;
        private bool _listeningKeys;
        private int _currentKey;
        private Mover _mover;
        private float _initialLightIntensity;

        protected override void Awake()
        {
            base.Awake();
            _initialLightIntensity = globalLight.intensity;
            interactable.OnInteract += Interact;
            OnActive += MakeSound;
        }

        protected override void Update()
        {
            base.Update();
            if (!_listeningKeys) return;
            
            if (Input.GetKeyDown(_currentFeeling.Keys[_currentKey].KeyCode))
            {
                keyCombinationDisplayer.CorrectKey(_currentKey);
                _currentKey++;
                if (_currentKey < _currentFeeling.Keys.Length) return;
                StopListeningToKeys();
                Satisfy();
            } else if (Input.anyKeyDown)
            {
                keyCombinationDisplayer.IncorrectKey(_currentKey);
            }
        }

        protected override void Need()
        {
            _currentFeeling = GetRandomFeeling();
            Sprite = _currentFeeling.Sprite;
            base.Need();
        }

        private Feeling GetRandomFeeling() => feelings[Random.Range(0, feelings.Length - 1)];

        private void MakeSound()
        {
            AudioManager.Instance.PlaySound(_currentFeeling.GetRandomAudioClip());
        }
        
        private void Interact(Interactor interactor)
        {
            if (!Active) return;
            if (_listeningKeys)
            {
                StopListeningToKeys();
                return;
            }
            _mover = interactor.GetComponent<Mover>();
            if (_mover == null) return;
            _mover.Active = false;
            keyCombinationDisplayer.ShowKeyCombination(_currentFeeling.Keys);
            _listeningKeys = true;
            HighlightProgrammer();
        }

        private void StopListeningToKeys()
        {
            _listeningKeys = false;
            _mover.Active = true;
            _currentKey = 0;
            keyCombinationDisplayer.HideKeyCombination();
            UnHighlightProgrammer();
        }

        private void HighlightProgrammer()
        {
            globalLight.intensity = lightIntensityWhenTalking;
        }

        private void UnHighlightProgrammer()
        {
            globalLight.intensity = _initialLightIntensity;
        }
    }
}