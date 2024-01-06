using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isPlayerAttack;
    public int maxDamage = 10, minDamage = 5;
    public GameObject audioSourceUtil;

    protected HUDController hudController;

    protected void Start()
    {
        if (isPlayerAttack)
        {
            GameObject hudControllerObject = GameObject.FindGameObjectWithTag(GlobalTags.HUD_CONTROLLER);
            hudController = hudControllerObject.GetComponent<HUDController>();
        }
    }

    protected void PlaySound(AudioClip soundClip, float timeToDestroy = 5)
    {
        GameObject audioInstance = Instantiate(
            audioSourceUtil,
            audioSourceUtil.transform.position,
            audioSourceUtil.transform.rotation
        );

        audioInstance.transform.parent = transform;

        AudioSourceUtil audioSource = audioInstance.GetComponent<AudioSourceUtil>();
        audioSource.PlaySound(soundClip, timeToDestroy);
    }

    int GetRandomDamage()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }

    public void ApplyDamage(GameObject other)
    {
        EntityController entityController = other.GetComponent<EntityController>();
        entityController.HaveHitADamage(GetRandomDamage(), gameObject);
    }
}
