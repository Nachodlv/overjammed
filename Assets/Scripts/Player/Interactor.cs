using DefaultNamespace;
using UnityEngine;
using Utils;
// ReSharper disable Unity.NoNullPropagation

namespace Player
{
    public class Interactor: MonoBehaviour
    {
        [SerializeField] private float interactReach;
        [SerializeField] private string interactableTag = "Interactable";
        
        private DistanceDetector _distanceDetector;
        private Interactable _currentHighlighted;
        private void Awake()
        {
            SetUpDistanceDetector();
        }

        private void Update()
        {
            if(Input.GetButtonDown("Fire1")) Interact();
        }

        private void Interact()
        {
            _currentHighlighted?.Interact();
        }

        private void HighlightNearestInteractable(Collider2D collider2D)
        {
            var interactable = collider2D.GetComponent<Interactable>();
            _currentHighlighted?.UnHighlight();
            interactable.Highlight();
            _currentHighlighted = interactable;
        }

        private void UnHighlightInteractable(Collider2D collider2D)
        {
            if(collider2D.gameObject == _currentHighlighted.gameObject) 
                _currentHighlighted?.UnHighlight();
        }
        
        private void SetUpDistanceDetector()
        {
            _distanceDetector = gameObject.AddComponent<DistanceDetector>();
            _distanceDetector.DetectionDistance = interactReach;
            _distanceDetector.targetTag = interactableTag;
            _distanceDetector.OnColliderInsideRadius += HighlightNearestInteractable;
            _distanceDetector.OnColliderOutsideRadius += UnHighlightInteractable;
        }
    }
}