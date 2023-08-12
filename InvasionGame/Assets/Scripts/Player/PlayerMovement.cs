using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 12;
    public SpriteRenderer playerSprite;
    public Transform playerGunsWrapper;

    float repulsionTime = 0.1f, fixedYPosition = 1;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
        PointGunsToMouse();
    }

    void MovePlayer()
    {
        Vector3 inputsValue = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        characterController.Move(inputsValue * walkSpeed * Time.deltaTime);
        transform.position = new Vector3(
            transform.position.x,
            fixedYPosition,
            transform.position.z
        );

        float xSpriteDirection = Mathf.Sign(inputsValue.x);

        playerSprite.flipX = xSpriteDirection < 0;
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
