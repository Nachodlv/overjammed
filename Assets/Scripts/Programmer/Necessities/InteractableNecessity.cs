using System;
using Interactables;
using Player;
using UnityEngine;

namespace Programmer.Necessities
{
    public class InteractableNecessity: Necessity
    {
        [SerializeField] private Interactable interactable;
        
        public override event NeedChange OnSatisfied;

        private void Awake()
        {
            interactable.OnInteract += Interact;
        }

        private void Interact(Interactor interactor)
        {
            if (CurrentNeed < MinimumNeed) return;
            
        }
    }
}