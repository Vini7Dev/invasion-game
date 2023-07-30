using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isPlayerAttack = false;
    public int minDamage = 3, maxDamage = 10;

    protected bool isFiregun;

    public bool IsFiregun()
    {
        return isFiregun;
    }
}
