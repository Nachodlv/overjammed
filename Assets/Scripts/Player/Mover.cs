using UnityEngine;

namespace Player
{ 
    [RequireComponent(typeof(Rigidbody2D) )]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Animator animator;

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _velocity;
        private static readonly int XSpeed = Animator.StringToHash("speed");

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
        
            _velocity = new Vector2(x, y) * speed;
            animator.SetFloat(XSpeed, Mathf.Abs(x + y));
            
           _spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        }

        private void FixedUpdate()
        {
            _rigidbody2D.position += _velocity * Time.fixedDeltaTime;
        }
    }
}
