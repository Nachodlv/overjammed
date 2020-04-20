using System;
using Interactables;
using Player;
using Sound;
using UnityEngine;

namespace Programmer.Necessities
{
    public class InteractableNecessity: Necessity
    {
        [SerializeField] private Interactable interactable;
        [SerializeField] private AudioClip satisfyClip;
        [SerializeField] private Animator animator;

        private bool _hasAnimator;
        private bool _hasSatisfyClip;
        private static readonly int NeedTrigger = Animator.StringToHash("need");
        private static readonly int SatisfyTrigger = Animator.StringToHash("satisfy");

        protected override void Awake()
        {
            base.Awake();
            interactable.OnInteract += Interact;
            _hasAnimator = animator != null;
            _hasSatisfyClip = satisfyClip != null;
            if(_hasAnimator) OnNeed += PlayAnimationOnNeed;
        }

        private void PlayAnimationOnNeed(Necessity necessity)
        {
            animator.SetTrigger(NeedTrigger);
        }
        
        private void Interact(Interactor interactor)
        {
            if (!Active || CurrentNeed < MinimumNeed) return;
            if(_hasSatisfyClip) AudioManager.Instance.PlaySound(satisfyClip);
            if(_hasAnimator) animator.SetTrigger(SatisfyTrigger);
            Satisfy();
        }
    }
}