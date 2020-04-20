using Interactables;
using Player;
using UnityEngine;

namespace Programmer.Necessities
{
    public class FetchNecessity : Necessity
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private Interactable interactable;

        protected override void Awake()
        {
            base.Awake();
            interactable.OnInteract += Interact;
        }

        private void Interact(Interactor interactor)
        {
            if (!Active || CurrentNeed < MinimumNeed) return;
            var grabber = interactor.GetComponent<Grabber>();
            if (grabber == null || !grabber.HasGrabbable || grabber.Grabbable.ItemType != itemType) return;
            grabber.TakeGrabbable();
            Satisfy();
        }
    }
}    