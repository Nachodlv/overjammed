using UnityEngine;

namespace Programmer
{
    public abstract class Necessity: MonoBehaviour
    {
        [SerializeField] private Sprite sprite;
        [SerializeField, Tooltip("How much it affect the stress level")]
        private float stressLevel;
        
        public delegate void NeedChange(Necessity necessityOnNeed);
        public abstract event NeedChange OnNeed;
        public abstract event NeedChange OnSatisfied;
        
        public Sprite Sprite => sprite;
        public float StressLevel => stressLevel;
    }
}