using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public bool followPlayer = false;
    public float cameraTopDistance = 25;
    public float cameraFollowSpeed = 5;

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (followPlayer)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        Vector3 positonToFollow = new Vector3(
            player.position.x,
            cameraTopDistance,
            player.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            positonToFollow,
            Time.deltaTime * cameraFollowSpeed
        );
    }
}
