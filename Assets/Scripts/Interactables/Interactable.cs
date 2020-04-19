using System;
using Player;
using Sound;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Interactables
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private Light2D pointLight;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioClip audioClip;
        public event Action<Interactor> OnInteract;

        private bool _hasAnimator;
        private bool _hasAudioClip;
        private static readonly int InteractTrigger = Animator.StringToHash("interact");

        protected virtual void Awake()
        {
            pointLight.enabled = false;
            _hasAnimator = animator != null;
            _hasAudioClip = audioClip != null;
        }

        public virtual void Interact(Interactor interactor)
        {
            OnInteract?.Invoke(interactor);
            if(_hasAnimator) animator.SetTrigger(InteractTrigger);
            if(_hasAudioClip) AudioManager.Instance.PlaySound(audioClip);
        }
        public void Highlight()
        {
            pointLight.enabled = true;
        }

        public void UnHighlight()
        {
            pointLight.enabled = false;
        }
    }
}