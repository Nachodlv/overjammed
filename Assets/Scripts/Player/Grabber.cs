using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Grabber : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        public Grabbable Grabbable { get; private set; }
        public bool HasGrabbable { get; private set; }

        private void Awake()
        {
            image.enabled = false;
        }

        public void Grab(Grabbable grabbable)
        {
            if (!grabbable.Active) return;
            if (!HasGrabbable) image.enabled = true;
            
            Grabbable = grabbable;
            HasGrabbable = true;
            image.sprite = grabbable.Sprite;
        }

        public void TakeGrabbable()
        {
            HasGrabbable = false;
            image.enabled = false;
        }
    }
}