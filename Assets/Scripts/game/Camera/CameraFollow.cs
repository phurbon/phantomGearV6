using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Assign the Player object here
    public float smoothSpeed = 5f; // Adjust for smoothness
    public Vector3 offset; // Adjust this to set the camera's position relative to the player

    void LateUpdate()
    {
        if (target == null)
            return;

        // Target position with offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate the camera position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
