using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWhiteGunHands : EntityWhiteGunHands
{
    public bool isSpinHandsAttack;
    public AudioClip spinHandsAttackSound;

    bool handsSpinning;
    float spinHandsSmoothSpeed = 0;
    Transform playerTransform;
    Vector3 yRotateDirection = new Vector3(0, 1, 0);

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.transform;
    }

    void Update()
    {
        if (isSpinHandsAttack) SpinHands();
        else PointToPlayer();
    }

    public void ToggleHandsSpin()
    {
        handsSpinning = !handsSpinning;

        if (handsSpinning) PlaySound(spinHandsAttackSound, 10);
    }

    void SpinHands()
    {
        if (handsSpinning && spinHandsSmoothSpeed < 3)
            spinHandsSmoothSpeed += Time.deltaTime * 1.5f;
        else if (!handsSpinning && spinHandsSmoothSpeed > 0)
            spinHandsSmoothSpeed -= Time.deltaTime * 1.5f;

        whiteGunHands.transform.Rotate(yRotateDirection * spinHandsSmoothSpeed);
    }

    void PointToPlayer()
    {
        if (playerTransform != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(
                playerTransform.position - whiteGunHands.transform.position
            );

            whiteGunHands.transform.rotation = Quaternion.Slerp(
                whiteGunHands.transform.rotation,
                targetRotation,
                Time.deltaTime * 4
            );
        }
    }
}
