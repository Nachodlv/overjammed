using System;
using System.Collections;
using DefaultNamespace;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interactables
{
    [RequireComponent(typeof(Animator), typeof(SortOrderModifier))]
    public class StressBall: MonoBehaviour
    {
        [SerializeField] private Interactable interactable;
        [SerializeField] private Grabbable grabbable;
        [SerializeField] private float minTimeBeforeThrow;
        [SerializeField] private float maxTimeBeforeThrow;

        public event Action OnThrow;
        
        private bool ballOnGround;
        private Func<IEnumerator> _waitToThrowBall;
        private Func<IEnumerator> _sendBallToDestination;
        private Animator animator;
        private static readonly int ThrowTrigger = Animator.StringToHash("throw");
        private Vector2 _initialPosition;
        private SortOrderModifier sortOrderModifier;

        private void Awake()
        {
            _initialPosition = transform.position;
            animator = GetComponent<Animator>();
            sortOrderModifier = GetComponent<SortOrderModifier>();
            interactable.OnInteract += Interact;
            _waitToThrowBall = WaitToThrowBall;
            StartCoroutine(_waitToThrowBall());
        }

        private void Start()
        {
            grabbable.Active = false;
        }

        public void ResetPosition()
        {
            gameObject.SetActive(true);
            transform.position = _initialPosition;
            sortOrderModifier.ExtraYAxis = -0.1f;
            sortOrderModifier.PositionChanged();
            ballOnGround = false;
            grabbable.Active = false;
            StartCoroutine(_waitToThrowBall());
        }

        private IEnumerator WaitToThrowBall()
        {
            yield return new WaitForSeconds(Random.Range(minTimeBeforeThrow, maxTimeBeforeThrow));
            ThrowBall();
        }

        private void ThrowBall()
        {
            OnThrow?.Invoke();
            animator.SetTrigger(ThrowTrigger);
        }

        private void AnimationFinished()
        {
            sortOrderModifier.ExtraYAxis = 0.2f;
            sortOrderModifier.PositionChanged();
            grabbable.Active = true;
            ballOnGround = true;
        }
        
        private void Interact(Interactor interactor)
        {
            if (!ballOnGround) return;
            gameObject.SetActive(false);
        }
        
    }
}