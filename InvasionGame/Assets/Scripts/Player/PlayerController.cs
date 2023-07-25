using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int life = 100;

    public void HaveHitADamage(int damageReceived)
    {
        life -= damageReceived;

        gameObject.SetActive(life > 0);
    }
}
