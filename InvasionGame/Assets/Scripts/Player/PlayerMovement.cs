using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    public float walkSpeed = 5;

    float fixedYPosition = 1;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
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
}
