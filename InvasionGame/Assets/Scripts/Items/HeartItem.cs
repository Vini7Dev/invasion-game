using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour
{
    int lifeToAdd = 25;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().life += lifeToAdd;
            Destroy(gameObject);
        }
    }
}
