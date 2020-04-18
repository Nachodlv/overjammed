using System;
using DefaultNamespace;
using Player;
using UnityEngine;

namespace Programmer.Necessities
{
    public class FetchNecessity : Necessity
    {
        [SerializeField] private ItemType itemType;
        public override event NeedChange OnSatisfied;

        private void Awake()
        {
            GetComponentInParent<NecessityController>().OnInteract += Interact;
        }

        private void Interact(Interactor interactor)
        {
            if (CurrentNeed < MinimumNeed) return;
            var grabber = interactor.GetComponent<Grabber>();
            if (grabber == null || !grabber.HasGrabbable || grabber.Grabbable.ItemType != itemType) return;
            grabber.TakeGrabbable();
            Satisfy();
        }
        
        private void Satisfy()
        {
            CurrentNeed = 0;
            InvokedNeed = false;
            OnSatisfied?.Invoke(this);
        }
    }
}