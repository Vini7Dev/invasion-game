using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public SpriteRenderer entitySprite;
    public int life = 100;

    bool onDamage;
    float damageTime = 0.2f;

    public bool IsAlive()
    {
        return life > 0;
    }

    public void HaveHitADamage(int damageReceived)
    {
        if (onDamage)
        {
            return;
        }

        life -= damageReceived;

        StartCoroutine(DamageTimer());

        if (life <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator DamageTimer()
    {
        onDamage = true;
        entitySprite.color = new Color(0.5f, 0.5f, 0.5f, 1f);

        yield return new WaitForSeconds(damageTime);

        entitySprite.color = new Color(1f, 1f, 1f, 1f);
        onDamage = false;
    }
}
