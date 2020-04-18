using System;
using Player;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Interactables
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private Light2D pointLight;

        public event Action<Interactor> OnInteract; 
        
        private void Awake()
        {
            pointLight.enabled = false;
        }

        public void Interact(Interactor interactor)
        {
            OnInteract?.Invoke(interactor);
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