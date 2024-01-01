using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer playerSprite;

    float fixedYPosition = 0.1f;
    CharacterController characterController;
    EntitySkills playerSkills;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        playerSkills = GetComponent<EntitySkills>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 inputsValue = new Vector3(
            Input.GetAxis(GlobalButtons.HORIZONTAL),
            0,
            Input.GetAxis(GlobalButtons.VERTICAL)
        );

        characterController.Move(inputsValue * playerSkills.moveSpeed * Time.deltaTime);
        transform.position = new Vector3(
            transform.position.x,
            fixedYPosition,
            transform.position.z
        );

        float xSpriteDirection = Mathf.Sign(inputsValue.x);

        playerSprite.flipX = xSpriteDirection < 0;
    }
}
