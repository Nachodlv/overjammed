using Player;
using UnityEngine;

namespace Programmer.Necessities
{
    public class FetchNecessity : Necessity
    {
        [SerializeField] private ItemType itemType;
        private void Awake()
        {
            GetComponentInParent<NecessityController>().OnInteract += Interact;
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