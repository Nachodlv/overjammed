using System;
using UnityEngine;

namespace Programmer
{
    public abstract class Necessity : MonoBehaviour
    {
        [SerializeField] private Sprite sprite;

        [SerializeField, Tooltip("How much it affect the stress level")]
        private float stressLevel;

        [SerializeField] private float startingNeed;
        [SerializeField] protected float increaseRatio = 5f;

        public delegate void NeedChange(Necessity necessityOnNeed);

        public event NeedChange OnNeed;
        public event NeedChange OnSatisfied;
        protected event Action OnActive;

        public Sprite Sprite
        {
            get => sprite;
            protected set => sprite = value;
        }

        public float StressLevel => stressLevel;

        public bool Active
        {
            get => _active;
            set
            {
                if (value) OnActive?.Invoke();
                _active = value;
            }
        }

        protected float CurrentNeed;
        protected float MinimumNeed = 50;
        private bool _invokedNeed;
        private bool _active;

        protected virtual void Awake()
        {
            CurrentNeed = startingNeed;
        }

        protected virtual void Update()
        {
            if (CurrentNeed >= 100) return;

            CurrentNeed += increaseRatio * Time.deltaTime;

            if (!_invokedNeed && CurrentNeed >= MinimumNeed)
            {
                Need();
            }
        }

        protected void Satisfy()
        {
            CurrentNeed = 0;
            _invokedNeed = false;
            OnSatisfied?.Invoke(this);
            Active = false;
        }

        protected virtual void Need()
        {
            OnNeed?.Invoke(this);
            _invokedNeed = true;
        }
    }
}