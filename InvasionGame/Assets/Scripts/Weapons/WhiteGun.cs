using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteGun : Weapon
{
    public LayerMask collisionLayer;
    public float detectionIntervalPerSecond = 0.3f;

    bool causedDamage;
    float damageDalay = 0.5f, distanceToCreateCollision = 0.2f;
    Vector3 previousPosition;

    void Start()
    {
        base.Start();

        if (isPlayerAttack)
        {
            hudController.ammoInfo.ClearAmmoInfo();
            previousPosition = transform.position;
            StartCoroutine(PlayerAttackCollisionDetectionRoutine());
        }
    }

    IEnumerator PlayerAttackCollisionDetectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(detectionIntervalPerSecond);

            Vector3 currentPosition = transform.position;
            float distance = Vector3.Distance(previousPosition, currentPosition);

            if (distance > distanceToCreateCollision)
            {
                Vector3 dimensions = new Vector3(
                    Mathf.Abs(previousPosition.x - currentPosition.x),
                    Mathf.Abs(previousPosition.y - currentPosition.y),
                    Mathf.Abs(previousPosition.z - currentPosition.z)
                );

                Vector3 midpoint = (previousPosition + currentPosition) / 2;

                Collider[] colliders = Physics.OverlapBox(midpoint, dimensions / 2, Quaternion.identity, collisionLayer);

                foreach (var collider in colliders)
                {
                    if (collider.tag == ENEMY_TAG || collider.tag == BREAKABLE_SCENERY_TAG)
                    {
                        ApplyDamage(collider.gameObject);
                    }
                }
            }

            previousPosition = currentPosition;
        }
    }

    IEnumerator PlayerDamageDelay()
    {
        causedDamage = true;
        yield return new WaitForSeconds(damageDalay);
        causedDamage = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag != PLAYER_TAG || causedDamage) return;

        StartCoroutine(PlayerDamageDelay());

        ApplyDamage(other.gameObject);
    }
}
