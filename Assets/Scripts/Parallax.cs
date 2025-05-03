using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float objectHeight;
    private Vector3 startPosition;
    public float parallaxEffect;
    [SerializeField] private float autoScrollSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Get height using sprite renderer
        if (GetComponent<SpriteRenderer>() != null) objectHeight = GetComponent<SpriteRenderer>().bounds.size.y;
        // Get height using box collider if sprite renderer doesn't exist
        else if (GetComponent<BoxCollider2D>() != null) objectHeight = GetComponent<BoxCollider2D>().bounds.size.y;

        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate scroll amount based on parallax effect
        float scrollAmount = autoScrollSpeed * Time.deltaTime;
        float parallaxScrollAmount = scrollAmount * parallaxEffect;
        
        // Move the background
        transform.Translate(Vector3.down * parallaxScrollAmount);

        float resetPosition = startPosition.y - objectHeight;

        // Reset position when object scrolls past the reset point
        if (transform.position.y < resetPosition) 
        {
            Vector3 newPosition = transform.position;
            newPosition.y += objectHeight;
            transform.position = newPosition;
        }
    }
}
