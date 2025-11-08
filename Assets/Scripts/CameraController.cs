using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The car game object
    public float orbitSpeed = 0.01f; // The Orbit revolution speed
    public float radius = 10.0f; // Radius of the circular path
    public float angleX = 0.0f; // Initial angle of the camera around X-axis
    public float angleY = 0.0f; // Initial angle of the camera around Y-axis
    public float minRadius = 5.0f; // Minimum radius for zooming
    public float maxRadius = 12.0f; // Maximum radius for zooming
    public float minYAngle = -10.0f; // Minimum angle for Y-axis rotation
    public float maxYAngle = 30.0f; // Maximum angle for Y-axis rotation

    private Vector3 cameraPosition; // 3D Vector Position

    void Start()
    {
        // Initialize the camera's position and angle
        cameraPosition = target.position + new Vector3(radius * Mathf.Cos(angleX), radius * Mathf.Sin(angleY), radius * Mathf.Sin(angleX));
        transform.position = cameraPosition;
        transform.LookAt(target);
    }

    void Update()
    {
        // Handle touch input
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.touches[0];

                // Orbit the camera
                if (touch.phase == TouchPhase.Moved)
                {
                    angleX += touch.deltaPosition.x * orbitSpeed;
                    angleY += touch.deltaPosition.y * orbitSpeed;
                    angleY = Mathf.Clamp(angleY, minYAngle * Mathf.Deg2Rad, maxYAngle * Mathf.Deg2Rad);
                    
                    cameraPosition = target.position + new Vector3(radius * Mathf.Cos(angleX), radius * Mathf.Sin(angleY), radius * Mathf.Sin(angleX));
                    transform.position = cameraPosition;
                    transform.LookAt(target);
                }
            }
            else if (Input.touchCount == 2)
            {
                Touch touch1 = Input.touches[0];
                Touch touch2 = Input.touches[1];

                // Zoom in and out
                if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
                {
                    float distance = Vector2.Distance(touch1.position, touch2.position);
                    float prevDistance = Vector2.Distance(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
                    float deltaDistance = distance - prevDistance;

                    radius -= deltaDistance * 0.01f;
                    radius = Mathf.Clamp(radius, minRadius, maxRadius);

                    cameraPosition = target.position + new Vector3(radius * Mathf.Cos(angleX), radius * Mathf.Sin(angleY), radius * Mathf.Sin(angleX));
                    transform.position = cameraPosition;
                    transform.LookAt(target);
                }
            }
        }
    }
}