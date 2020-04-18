using UnityEngine;

namespace Programmer.Necessities
{
    public class Hungry : Necessity
    {
        [SerializeField] private float decreaseRatio;
        
        public override event NeedChange OnNeed;
        public override event NeedChange OnSatisfied;

        private float _currentNeed = 100;
        private float _minimumNeed = 50;
        private bool _invokedNeed;

        private void Update()
        {
            if (_currentNeed <= 0)
            {
                Feed();
                return;
            }
            Debug.Log($"Hungry: {_currentNeed}");
            
            _currentNeed -= decreaseRatio * Time.deltaTime;
            
            if (!_invokedNeed && _currentNeed <= _minimumNeed)
            {
                OnNeed?.Invoke(this);
                _invokedNeed = true;
            }
        }

        private void Feed()
        {
            _currentNeed = 100;
            _invokedNeed = false;
            OnSatisfied?.Invoke(this);
        }
    }
}