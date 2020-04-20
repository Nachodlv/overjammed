using Interactables;
using Player;
using UnityEngine;

namespace Programmer.Necessities
{
    public class StressBallNecessity: Necessity
    {
        [SerializeField] private StressBall stressBall;
        [SerializeField] private Interactable interactable;

        private bool ballThrew;

        protected override void Awake()
        {
            base.Awake();
            interactable.OnInteract += Interact;
            stressBall.OnThrow += BallThrew;
            increaseRatio = 0;
        }

        private void BallThrew()
        {
            Need();
            ballThrew = true;
        }

        private void Interact(Interactor interactor)
        {
            if (!ballThrew) return;
            var grabber = interactor.GetComponent<Grabber>();
            if (grabber == null || !grabber.HasGrabbable || grabber.Grabbable.ItemType != ItemType.StressBall) return;
            grabber.TakeGrabbable();
            stressBall.ResetPosition();
            ballThrew = false;
            Satisfy();
        }
    }
}