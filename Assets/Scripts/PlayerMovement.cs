using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        // Input for movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Process mouse rotation
        RotateToMouse();
    }

    void FixedUpdate()
    {
        // Movement mechanics
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void RotateToMouse()
    {
        // Convert mouse screen position into world position
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; // Ensure that the position is not in the 3D space (z-axis)

        // Calculate direction from player position to mouse position
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        // Calculate the angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; // we subtract 90 degrees to compensate for the sprite orientation

        // Apply rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
