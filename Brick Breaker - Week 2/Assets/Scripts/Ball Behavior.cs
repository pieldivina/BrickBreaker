using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]

public class BallBehavior : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _prevBallVelocity;
    private bool _isPaused = false;
    
    private AudioSource _source;
    
    [SerializeField] private AudioClip _wallHitClip;
    [SerializeField] private AudioClip _paddleHitClip;
    [SerializeField] private AudioClip _brickHitClip;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _source = GetComponent<AudioSource>();
        _rb.gravityScale = 0.0f;

        ResetBall();
    }
    
    private void Update()
    {
        if (GameBehavior.Instance.CurrentState == Utilities.GameState.Pause)
        {
            if (!_isPaused)
            {
                _prevBallVelocity = _rb.linearVelocity;
                _rb.linearVelocity = Vector2.zero;

                _isPaused = true;
            }
            
        }
        else
        {
            if (_isPaused)
            {
                _rb.linearVelocity = _prevBallVelocity;
                _isPaused = false;
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (GameBehavior.Instance.CurrentState == Utilities.GameState.Pause)
            return;

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
            }
            
            _rb.linearVelocity *= GameBehavior.Instance.BallSpeedIncrement;
            PlaySound(_paddleHitClip);
        }
        else
        {
            PlaySound(_wallHitClip, pitchMax: 1.2f);
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

    void PlaySound(AudioClip clip, float pitchMin = 1.0f, float pitchMax = 2.0f)
    {
        _source.clip = clip;
        _source.pitch = Random.Range(pitchMin, pitchMax);
        _source.Play();
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
