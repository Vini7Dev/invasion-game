using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public int life = 30;

    public void HaveHitADamage(int damageReceived)
    {
        life -= damageReceived;

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
