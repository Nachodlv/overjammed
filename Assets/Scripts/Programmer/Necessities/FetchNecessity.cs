using System;
using DefaultNamespace;
using Player;
using UnityEngine;

namespace Programmer.Necessities
{
    public class FetchNecessity : Necessity
    {
        [SerializeField] private float increaseRatio = 5f;
        [SerializeField] private ItemType itemType;
        public override event NeedChange OnNeed;
        public override event NeedChange OnSatisfied;

        private float _currentNeed;
        private float _minimumNeed = 50;
        private bool _invokedNeed;
        
        private void Awake()
        {
            GetComponentInParent<NecessityController>().OnInteract += Interact;
        }

        private void Update()
        {
            if (_currentNeed >= 100)
            {
                Feed();
                return;
            }
            Debug.Log($"Hungry: {_currentNeed}");
            
            _currentNeed += increaseRatio * Time.deltaTime;
            
            if (!_invokedNeed && _currentNeed >= _minimumNeed)
            {
                OnNeed?.Invoke(this);
                _invokedNeed = true;
            }
        }

        private void Interact(Interactor interactor)
        {
            if (_currentNeed < _minimumNeed) return;
            var grabber = interactor.GetComponent<Grabber>();
            if (grabber == null || !grabber.HasGrabbable || grabber.Grabbable.ItemType != itemType) return;
            grabber.TakeGrabbable();
            Feed();
        }
        
        private void Feed()
        {
            _currentNeed = 0;
            _invokedNeed = false;
            OnSatisfied?.Invoke(this);
        }
    }
}