using System;
using Player;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Interactables
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private Light2D pointLight;
        [SerializeField] private Animator animator;
        public event Action<Interactor> OnInteract;

        private bool _hasAnimator;
        private static readonly int InteractTrigger = Animator.StringToHash("interact");

        private void Awake()
        {
            pointLight.enabled = false;
            _hasAnimator = animator != null;
        }

        public void Interact(Interactor interactor)
        {
            OnInteract?.Invoke(interactor);
            if(_hasAnimator) animator.SetTrigger(InteractTrigger);
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