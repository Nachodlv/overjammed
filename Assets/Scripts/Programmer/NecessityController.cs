using System;
using System.Collections.Generic;
using DefaultNamespace;
using Interactables;
using Player;
using UnityEngine;

namespace Programmer
{
    [RequireComponent(typeof(Interactable))]
    public class NecessityController: MonoBehaviour
    {
        public const float MAX_STRESS_LEVEL = 100;

        [SerializeField] private NecessityDisplayer _necessityDisplayer;
        [SerializeField] private float stressDecreaseRatio = 1f;

        public event Action<Interactor> OnInteract;
        public float StressLevel { get; private set; }

        private Necessity[] _necessities;
        private List<Necessity> _necessitiesOnNeed;
        
        private void Awake()
        {
            _necessities = GetComponentsInChildren<Necessity>();
            _necessitiesOnNeed = new List<Necessity>(_necessities.Length);
            GetComponent<Interactable>().OnInteract += OnInteract;
            SubscribeToOnNeed();
        }

        private void Update()
        {
            UpdateStressLevel();
        }

        private void SubscribeToOnNeed()
        {
            foreach (var necessity in _necessities)
            {
                necessity.OnNeed += NecessityOnNeed;
                necessity.OnSatisfied += NecessitySatisfied;
            }
        }

        private void NecessityOnNeed(Necessity necessity)
        {
            if(_necessitiesOnNeed.Count == 0) _necessityDisplayer.DisplayNecessity(necessity);
            _necessitiesOnNeed.Add(necessity);
        }

        private void NecessitySatisfied(Necessity necessity)
        {
            _necessitiesOnNeed.Remove(necessity);
            if(_necessitiesOnNeed.Count > 0) 
                _necessityDisplayer.DisplayNecessity(_necessitiesOnNeed[0]);
            else _necessityDisplayer.HideNecessity();
        }

        private void UpdateStressLevel()
        {
            if (_necessitiesOnNeed.Count == 0)
            {
                var newStress = StressLevel - stressDecreaseRatio * Time.deltaTime;
                StressLevel = newStress < 0 ? 0 : newStress;
                return;
            }
            for (var i = 0; i < _necessitiesOnNeed.Count; i++)
            {
                StressLevel += _necessitiesOnNeed[i].StressLevel * Time.deltaTime;
            }
            Debug.Log($"Stress level: {StressLevel}");
            if(StressLevel >= MAX_STRESS_LEVEL) Debug.Log("Game Over");
        }
    }
}