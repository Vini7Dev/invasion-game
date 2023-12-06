using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float moveSpeed = 50f, yPosition = 9.5f, zPosition = -2.5f;
    Vector3 cameraPosition;

    void Start()
    {
        cameraPosition = new Vector3(0, yPosition, zPosition);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            cameraPosition,
            moveSpeed * Time.deltaTime
        );
    }

    public void UpdateCameraPosition(Vector2 newCameraPosition)
    {
        cameraPosition = new Vector3(newCameraPosition.x, yPosition, newCameraPosition.y + zPosition);
    }
}
