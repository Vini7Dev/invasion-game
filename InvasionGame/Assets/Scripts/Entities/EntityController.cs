using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public SpriteRenderer entitySprite;
    public int life = 100;

    protected bool onDamage;
    protected float damageTime = 0.2f;

    protected void Update()
    {
        YPositionCorrection();
    }

    private void YPositionCorrection() {
        Vector3 positionCorrect = new Vector3(
            transform.position.x,
            0.1f,
            transform.position.z
        );

        transform.position = positionCorrect;
    }

    protected virtual void WhenDying()
    {
        gameObject.SetActive(false);
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
            WhenDying();
        }
    }

    protected virtual IEnumerator DamageTimer()
    {
        onDamage = true;
        entitySprite.color = new Color(0.5f, 0.5f, 0.5f, 1f);

        yield return new WaitForSeconds(damageTime);

        entitySprite.color = new Color(1f, 1f, 1f, 1f);
        onDamage = false;
    }
}
