using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public Vector2 speed = new(5.0f, 5.0f); // Speed of the ball
    
    // Limits for the ball
    public float limitX = 9f;  
    public float limitY = 4.7f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ternary operator
        // condition ? passing : failing;
        speed.x *= Random.value < 0.5 ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position + (Vector3)speed * Time.deltaTime;

        if (newPosition.x <= -limitX || newPosition.x >= limitX) // If ball reaches horizontal limits
        { 
            speed.x *= -1; // Direction is reversed
            newPosition.x = Mathf.Clamp(newPosition.x, -limitX, limitX);
        }
        
        if (newPosition.y >= limitY) // If ball reaches top limit
        {
            speed.y *= -1; // Direction is reversed 
            newPosition.y = limitY;
        }

        // Apply new position
        transform.position = newPosition;
        // transform.position += (Vector3)speed * Time.deltaTime; 
        
    }
}
