using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace DefaultNamespace
{
    public class Interactable : UnityEngine.MonoBehaviour
    {
        [SerializeField] private Light2D pointLight;


        private void Awake()
        {
            pointLight.enabled = false;
        }

        public void Interact()
        {
                
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