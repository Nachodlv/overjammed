using System;
using UnityEngine;
using UnityEngine.UI;

namespace Programmer
{
    public class NecessityDisplayer : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void DisplayNecessity(Necessity necessity)
        {
            gameObject.SetActive(true);
            image.sprite = necessity.Sprite;
        }

        public void HideNecessity()
        {
            gameObject.SetActive(false);
        }
    }
}