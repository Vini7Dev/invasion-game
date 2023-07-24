using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    int bulletSpeed = 45;
    Vector3 forwardDirection = Vector3.forward;

    void Start()
    {
        Destroy(gameObject, 3);        
    }

    void Update()
    {
        transform.Translate(forwardDirection * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Scenary")
        {
            Destroy(gameObject);
        }
    }
}
