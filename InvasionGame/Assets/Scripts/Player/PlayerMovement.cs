using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12;
    public Transform playerSpriteTransform;
    public Transform playerGunsWrapper;

    float playerScale;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        playerScale = playerSpriteTransform.localScale.x;
    }

    void Update()
    {
        MovePlayer();
        PointGunsToMouse();
    }

    void MovePlayer()
    {
        Vector3 inputsValue = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        float xSpriteDirection = Mathf.Sign(inputsValue.x) * playerScale;

        characterController.Move(inputsValue * speed * Time.deltaTime);

        playerSpriteTransform.localScale = new Vector3(xSpriteDirection, playerScale, playerScale);
    }

    void PointGunsToMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        if (plane.Raycast(cameraRay, out distance))
        {
            Vector3 target = cameraRay.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            playerGunsWrapper.rotation = Quaternion.Euler(0, rotation, Time.deltaTime * 0.5f);
        }
    }
}
