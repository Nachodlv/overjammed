using UnityEngine;

namespace Player
{ 
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Rigidbody2D _rigidbody2D;
        private Vector2 _velocity;
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
        
            _velocity = new Vector2(x, y) * speed;
        }

        private void FixedUpdate()
        {
            _rigidbody2D.position += _velocity * Time.fixedDeltaTime;
        }
    }
}
