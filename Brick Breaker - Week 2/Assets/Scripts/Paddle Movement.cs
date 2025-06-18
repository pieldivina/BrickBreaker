    using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;

    private float _direction = 0.0f;
    
    [SerializeField] private KeyCode _rightDirection;
    [SerializeField] private KeyCode _leftDirection;
    
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        _rb.linearDamping = 0.0f;
        _rb.angularDamping = 0.0f; 
        _rb.gravityScale = 0.0f; 
    }

    void FixedUpdate()
    {
        _rb.linearVelocityX = _direction * _speed;
    }

    void Update()
    {
        _direction = 0.0f;
        
        if (Input.GetKey(_rightDirection)) _direction += 1.0f;
        if (Input.GetKey(_leftDirection)) _direction -= 1.0f;
    }
}
