using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isPlayerAttack = false;
    public int minDamage = 3, maxDamage = 10;
    public float attackTime = 0.5f;
    
    protected bool isFiregun;
	protected Animator weaponAnimator;

    protected void Start()
    {
        weaponAnimator = GetComponent<Animator>();
    }

    public bool IsFiregun()
    {
        return isFiregun;
    }
}
