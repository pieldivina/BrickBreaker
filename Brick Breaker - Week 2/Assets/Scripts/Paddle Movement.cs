using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    public float speed = 8.0f;

    public float limitX = 8.35f;

    public KeyCode rightDirection;
    public KeyCode leftDirection;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movement = 0.0f;
        
        // Check if the given keycode is currently pressed
        if (Input.GetKey(rightDirection))  
        {
            // Update position by adding two vectors
            movement += speed;
        }

        if (Input.GetKey(leftDirection))
        {
            // Update position by adding two vectors
            movement -= speed;
        }

        // Create new position within defined bounds
        Vector3 newPosition = transform.position + new Vector3(movement, 0.0f, 0.0f) * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x,-limitX, limitX);
        
        // Apply new position
        transform.position = newPosition;
    }
}
