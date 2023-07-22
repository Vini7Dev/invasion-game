using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
        LookAtMousePosition();
    }

    void MovePlayer()
    {
        Vector3 inputsValue = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        characterController.Move(inputsValue * speed * Time.deltaTime);
    }

    void LookAtMousePosition()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        if (plane.Raycast(cameraRay, out distance))
        {
            Vector3 target = cameraRay.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, rotation, Time.deltaTime * 0.5f);
        }
    }
}
