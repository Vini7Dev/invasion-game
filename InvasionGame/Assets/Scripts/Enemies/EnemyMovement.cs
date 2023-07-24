using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float maxPlayerDistance = 10,
        minPlayerDistance = 5,
        walkSpeed = 5,
        lookSpeed = 5;

    CharacterController characterController;
    Transform playerTransform;
    Vector3 lookDirection;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lookDirection = transform.position;
    }

    float GetPlayerDistance()
    {
        return Vector3.Distance(transform.position, playerTransform.position);
    }

    void Update()
    {
        if (GetPlayerDistance() <= maxPlayerDistance)
        {
            LookToPlayer();

            if (GetPlayerDistance() > minPlayerDistance)
            {
                WalkToPlayer();
            }
            else
            {
                MoveAwaiFromPlayer();
            }
        }
    }

    void LookToPlayer()
    {
        lookDirection = Vector3.Lerp(
            lookDirection,
            playerTransform.position,
            Time.deltaTime * lookSpeed
        );

        transform.LookAt(lookDirection); 
    }

    Vector3 GetDirectionToMove(float walkSpeed)
    {
        return transform.forward * Time.deltaTime * walkSpeed;
    }

    void WalkToPlayer()
    {
        characterController.Move(GetDirectionToMove(walkSpeed));
    }

    void MoveAwaiFromPlayer()
    {
        float goBackSpeed = -(walkSpeed - 2);    
    
        characterController.Move(GetDirectionToMove(goBackSpeed));
    }
}
