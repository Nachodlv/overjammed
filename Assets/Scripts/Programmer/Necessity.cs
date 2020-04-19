using UnityEngine;

namespace Programmer
{
    public abstract class Necessity: MonoBehaviour
    {
        [SerializeField] private Sprite sprite;
        [SerializeField, Tooltip("How much it affect the stress level")]
        private float stressLevel;
        [SerializeField] protected float increaseRatio = 5f;

        public delegate void NeedChange(Necessity necessityOnNeed);
        public event NeedChange OnNeed;
        public event NeedChange OnSatisfied;
        public Sprite Sprite => sprite;
        public float StressLevel => stressLevel;
        public bool Active { get; set; }

        protected float CurrentNeed;
        protected float MinimumNeed = 50;
        protected bool InvokedNeed;
        
        private void Update()
        {
            if (CurrentNeed >= 100) return;
            
            CurrentNeed += increaseRatio * Time.deltaTime;
            
            if (!InvokedNeed && CurrentNeed >= MinimumNeed)
            {
                Need();
            }
        }
        
        protected void Satisfy()
        {
            CurrentNeed = 0;
            InvokedNeed = false;
            OnSatisfied?.Invoke(this);
            Active = false;
        }

        protected void Need()
        {
            OnNeed?.Invoke(this);
            InvokedNeed = true;
        }
    }
}