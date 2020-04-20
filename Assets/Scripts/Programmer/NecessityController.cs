using System;
using System.Collections;
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
        public float StressLevel { get; private set; }
        public event Action OnMaxStressLevel;
        
        private Necessity[] _necessities;
        private List<Necessity> _necessitiesOnNeed;
        private Func<Necessity, IEnumerator> _activeNextNecessity;
        private ProgrammerAnimator _programmerAnimator;
        
        private void Awake()
        {
            _necessities = GetComponentsInChildren<Necessity>();
            _programmerAnimator = GetComponentInChildren<ProgrammerAnimator>();
            _activeNextNecessity = ActiveNextNecessity;
            _necessitiesOnNeed = new List<Necessity>(_necessities.Length);
            SubscribeToOnNeed();
        }

        private void Start()
        {
            stressDecreaseRatio /= LevelManager.Instance.Level;
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
            if (_necessitiesOnNeed.Count == 0)
            {
                _necessityDisplayer.DisplayNecessity(necessity);
                necessity.Active = true;
                _programmerAnimator.PutArmsUp();
            }
            _necessitiesOnNeed.Add(necessity);
        }

        private void NecessitySatisfied(Necessity necessity)
        {
            _necessitiesOnNeed.Remove(necessity);
            if (_necessitiesOnNeed.Count > 0)
            {
                _necessityDisplayer.DisplayNecessity(_necessitiesOnNeed[0]);
                StartCoroutine(_activeNextNecessity(_necessitiesOnNeed[0]));
                return;
            }
            _necessityDisplayer.HideNecessity();
            _programmerAnimator.PutArmsDown();
        }

        private void UpdateStressLevel()
        {
            if(StressLevel >= MAX_STRESS_LEVEL) OnMaxStressLevel?.Invoke();
            
            var newStress = StressLevel - stressDecreaseRatio * Time.deltaTime;
            StressLevel = newStress < 0 ? 0 : newStress;
            var currentLevel = LevelManager.Instance.Level;
            for (var i = 0; i < _necessitiesOnNeed.Count; i++)
            {
                StressLevel += _necessitiesOnNeed[i].StressLevel * Time.deltaTime * (currentLevel > 2? currentLevel/2:1);
            }
        }

        private IEnumerator ActiveNextNecessity(Necessity necessity)
        {
            yield return null;
            necessity.Active = true;
        }
    }
}