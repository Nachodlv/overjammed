using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform _transform;
    private Vector2 _velocity;
    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        
        _velocity = new Vector2(x, y) * speed;
    }

    private void FixedUpdate()
    {
        _transform.position += (Vector3) _velocity * Time.fixedDeltaTime;
    }
}
