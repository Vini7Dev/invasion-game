using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public GameObject audioSourceUtil;
    public AudioClip collectSound;
    public int moneyValue = 1;

    void PlayCollectSound()
    {
        GameObject audioInstance = Instantiate(
            audioSourceUtil,
            audioSourceUtil.transform.position,
            audioSourceUtil.transform.rotation
        );

        audioInstance.GetComponent<AudioSourceUtil>().PlaySound(collectSound);
    }

    void CollectMoney()
    {
        GameObject levelControllerObject = GameObject.FindGameObjectWithTag(GlobalTags.LEVEL_CONTROLLER);

        LevelController levelController = levelControllerObject.GetComponent<LevelController>();

        levelController.IncrementMoneyValue(moneyValue);

        PlayCollectSound();

        Destroy(gameObject);
    }

    void MoveToPlayerPosition(Vector3 playerPosition)
    {
        Vector3 moveDirection = playerPosition - transform.position;

        if (moveDirection.magnitude <= 0.1f) return;

        moveDirection.Normalize();

        transform.position += moveDirection * Time.deltaTime * 8;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag != GlobalTags.PLAYER) return;

        float distanceFromPlayer = Vector3.Distance(transform.position, other.transform.position);

        if (distanceFromPlayer < 0.5f) CollectMoney();
        else MoveToPlayerPosition(other.transform.position);
    }
}
