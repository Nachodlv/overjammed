using DefaultNamespace;
using Interactables;
using UnityEngine;
using Utils;
// ReSharper disable Unity.NoNullPropagation

namespace Player
{
    [RequireComponent(typeof(Grabber))]
    public class Interactor: MonoBehaviour
    {
        [SerializeField] private float interactReach;
        [SerializeField] private string interactableTag = "Interactable";
        
        public Grabber Grabber { get; private set; }
        
        private DistanceDetector _distanceDetector;
        private Interactable _currentHighlighted;
        private bool _hasHighlighted;
        private Transform _transform;
        private void Awake()
        {
            _transform = transform;
            Grabber = GetComponent<Grabber>();
            SetUpDistanceDetector();
        }

        private void Update()
        {
            if(Input.GetButtonDown("Fire1")) Interact();
            HighlightNearestInteractable();
        }

        private void Interact()
        {
            if (!_hasHighlighted) return;
            _currentHighlighted.Interact(this);
            var grabbable = _currentHighlighted.GetComponent<Grabbable>();
            if(grabbable != null)
                Grabber.Grab(grabbable);
        }

        private void HighlightNearestInteractable()
        {
            if (_distanceDetector.GetColliders().Count == 0) return;
            var interactable = _distanceDetector.GetNearestCollider(_transform.position).GetComponent<Interactable>();
            if(_hasHighlighted) _currentHighlighted.UnHighlight();
            interactable.Highlight();
            _currentHighlighted = interactable;
            _hasHighlighted = true;
        }

        private void UnHighlightInteractable(Collider2D collider2D)
        {
            if (_hasHighlighted && collider2D.gameObject == _currentHighlighted.gameObject)
            {
                _currentHighlighted?.UnHighlight();
                _hasHighlighted = false;
            }
        }
        
        private void SetUpDistanceDetector()
        {
            _distanceDetector = gameObject.AddComponent<DistanceDetector>();
            _distanceDetector.DetectionDistance = interactReach;
            _distanceDetector.targetTag = interactableTag;
            _distanceDetector.OnColliderOutsideRadius += UnHighlightInteractable;
        }
    }
}