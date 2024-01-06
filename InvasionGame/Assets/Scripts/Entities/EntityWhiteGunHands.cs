using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityWhiteGunHands : MonoBehaviour
{
    public GameObject whiteGunHands, whiteGunLeftHand, audioSourceUtil;

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
}
