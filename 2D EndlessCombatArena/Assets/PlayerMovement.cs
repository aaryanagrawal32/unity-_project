using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 2f;

    [Header("Boundary")]
    // Adjust these numbers in the Inspector to fit your specific background!
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4.5f;
    public float maxY = 4.5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private PlayerControls controls; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        // 1. Read Input
        moveInput = controls.Gameplay.Move.ReadValue<Vector2>();

        // 2. Face Direction (SMART VERSION)
        // This keeps your Size (5, 3, etc) and only changes the +/- sign
        Vector3 currentScale = transform.localScale;

        if (moveInput.x < 0)
        {
            // Face Left: Make X negative, keep Y and Z the same
            currentScale.x = -Mathf.Abs(currentScale.x);
            transform.localScale = currentScale;
        }
        else if (moveInput.x > 0)
        {
            // Face Right: Make X positive, keep Y and Z the same
            currentScale.x = Mathf.Abs(currentScale.x);
            transform.localScale = currentScale;
        }
    }

    private void FixedUpdate()
    {
        // 3. Sprint Logic
        float currentSpeed = moveSpeed;
        if (controls.Gameplay.Sprint.IsPressed())
        {
            currentSpeed *= sprintMultiplier;
        }

        // 4. Move Physics
        rb.linearVelocity = moveInput * currentSpeed;
    }

    // 5. THIS KEEPS PLAYER ON SCREEN
    private void LateUpdate() 
    {
        Vector3 currentPos = transform.position;

        // "Clamp" checks if the number is too big or too small, and fixes it.
        currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);
        currentPos.y = Mathf.Clamp(currentPos.y, minY, maxY);

        transform.position = currentPos;
    }
}