using UnityEngine;
using Random = UnityEngine.Random;
public class BallBehavior : MonoBehaviour
{
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

        if (Mathf.Abs(velocity.y) < GameBehavior.Instance._minYVelocity)
        {
            float newY = GameBehavior.Instance._minYVelocity * Mathf.Sign(velocity.y != 0 ? velocity.y : 1);
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
             
                Vector2 direction = _rb.linearVelocity * (1 - GameBehavior.Instance.PaddleInfluence)
                                    + other.rigidbody.linearVelocity * GameBehavior.Instance.PaddleInfluence;
                
                _rb.linearVelocity = _rb.linearVelocity.magnitude * direction.normalized;
                
                _rb.linearVelocity *= GameBehavior.Instance.BallSpeedIncrement;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ResetZone"))
        {
            GameBehavior.Instance.ResetGame();
            ResetBall();
        }
    }

    void ResetBall()
    {
        _rb.linearVelocity = Vector2.zero;
        transform.position = new Vector3(0.0f, -4.02f, 0.0f); 
        
        Vector2 direction = new Vector2(
            Utilities.GetNonZeroRandomFloat(),
            Utilities.GetNonZeroRandomFloat()
        ).normalized;
        
        _rb.AddForce(direction * GameBehavior.Instance.InitBallForce,ForceMode2D.Impulse);
    }
}
