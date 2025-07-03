using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;

        _health--;

        if (_health <= 0)
        {
            GameBehavior.Instance.ScorePoint();
            Destroy(gameObject);
        }
        else
        {
            UpdateColor();
        }
    }

    private void UpdateColor()
    {
        if (_spriteRenderer == null) return;

        switch (_health)
        {
            case 3:
                _spriteRenderer.color = Color.red;
                break;
            case 2:
                _spriteRenderer.color = Color.yellow;
                break;
            case 1:
                _spriteRenderer.color = Color.green;
                break;
            default:
                _spriteRenderer.color = Color.gray;
                break;
        }
    }
}    