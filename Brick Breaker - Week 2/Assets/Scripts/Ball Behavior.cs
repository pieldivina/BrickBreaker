using UnityEngine;
using Random = UnityEngine.Random;
public class BallBehavior : MonoBehaviour
{
    [SerializeField] private float _launchForce = 5.0f;
    [SerializeField] private float _paddleInfluence = 0.3f;
    [SerializeField] private float _ballSpeedIncrement = 0.9f;
    [SerializeField] private float _minYVelocity = 2.1f;
    
    private Rigidbody2D _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0.0f;

        ResetBall();
    }
    
    private void FixedUpdate()
    {
        Vector2 velocity = _rb.linearVelocity;

        if (Mathf.Abs(velocity.y) < _minYVelocity)
        {
            float newY = _minYVelocity * Mathf.Sign(velocity.y != 0 ? velocity.y : 1);
            velocity.y = newY;
            _rb.linearVelocity = velocity;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            if (!Mathf.Approximately(other.rigidbody.linearVelocityX, 0.0f))
            {
             
                Vector2 direction = _rb.linearVelocity * (1 - _paddleInfluence)
                                    + other.rigidbody.linearVelocity * _paddleInfluence;
                
                _rb.linearVelocity = _rb.linearVelocity.magnitude * direction.normalized * _ballSpeedIncrement;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ScoreZone"))
        {
            ResetBall();
        }
    }

    void ResetBall()
    {
        _rb.linearVelocity = Vector2.zero;
        transform.position = new Vector3(0.0f, -4.02f, 0.0f); 
        
        Vector2 direction = new Vector2(
            GetNonZeroRandomFloat(),
            Mathf.Abs(GetNonZeroRandomFloat())
        ).normalized;
        
        _rb.AddForce(direction * _launchForce,ForceMode2D.Impulse);
    }
    float GetNonZeroRandomFloat(float min = -1.0f, float max = 1.0f)
    {
        float num;
                
        do     
        {
            num = Random.Range(min, max);
        } while (Mathf.Approximately(num,0.0f));
           
        return num;

    }
    
}
