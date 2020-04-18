using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Grabber : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        private Grabbable _grabbable;
        private bool _hasGrabbable;

        private void Awake()
        {
            image.enabled = false;
        }

        public void Grab(Grabbable grabbable)
        {
            if (!_hasGrabbable) image.enabled = true;
            
            _grabbable = grabbable;
            _hasGrabbable = true;
            image.sprite = grabbable.Sprite;
        }
    }
}