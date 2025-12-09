using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // We will drag the Player here
    public float smoothSpeed = 0.125f; // How smooth the camera moves
    public Vector3 offset = new Vector3(0, 0, -10); // Keeps camera above the game

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate where the camera should be
            Vector3 desiredPosition = target.position + offset;
            
            // Smoothly move from current position to desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            // Apply the new position
            transform.position = smoothedPosition;
        }
    }
}