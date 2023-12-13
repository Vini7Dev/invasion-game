using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestController : MonoBehaviour
{
    const string PLAYER_TAG = "Player";

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void CollectTreasureChest()
    {
        animator.SetTrigger("OpenTreasureChest");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == PLAYER_TAG) CollectTreasureChest();
    }
}
