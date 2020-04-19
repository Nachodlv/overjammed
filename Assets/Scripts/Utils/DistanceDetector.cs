using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UIElements;

namespace Utils
{
    public class DistanceDetector: MonoBehaviour
    {
        public event Action<Collider2D> OnColliderInsideRadius;
        public event Action<Collider2D> OnColliderOutsideRadius;

        public float DetectionDistance
        {
            get => _circleCollider.radius;
            set => _circleCollider.radius = value;
        }

        public string targetTag;
        
        private List<Transform> _colliders;
        private CircleCollider2D _circleCollider;
        private Transform _transform;
        
        private void Awake()
        {
            _colliders = new List<Transform>(5);
            _transform = transform;
            SetUpSphereCollider();
        }

        public List<Transform> GetColliders()
        {
            return _colliders;
        }

        public void ResetColliders()
        {
            _colliders.Clear();
        }

        public Transform GetNearestCollider(Vector2 position)
        {
            if (_colliders.Count == 0) return null;
            if (_colliders.Count == 1) return _colliders[0];
            
            var minDistance = float.MaxValue;
            var colliderIndex = 0;
            for (var i = 0; i < _colliders.Count; i++)
            {
                var distance = Vector2.Distance(_colliders[i].position, position);
                if(distance > minDistance) continue;
                minDistance = distance;
                colliderIndex = i;
            }

            return _colliders[colliderIndex];
        }
        
        public void ChangeDetectionDistance(float newDistance)
        {
            if (newDistance < _circleCollider.radius)
            {
                for (var i = 0; i < _colliders.Count; i++)
                {
                    if (Vector3.Distance(_colliders[i].position, _transform.position) < newDistance) continue;
                    _colliders.RemoveAt(i);
                    i--;
                }
            }
            
            _circleCollider.radius = newDistance;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(targetTag)) return;
            OnColliderInsideRadius?.Invoke(other);
            _colliders.Add(other.transform);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag(targetTag)) return;
            OnColliderOutsideRadius?.Invoke(other);
            _colliders.Remove(other.transform);
        }

        private void SetUpSphereCollider()
        {
            _circleCollider = gameObject.AddComponent<CircleCollider2D>();
            _circleCollider.isTrigger = true;
            _circleCollider.radius = DetectionDistance;
        }
    }
}