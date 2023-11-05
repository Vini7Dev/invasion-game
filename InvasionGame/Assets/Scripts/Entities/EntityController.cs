using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    protected const string BREAKABLE_SCENERY_TAG = "BreakableScenery";
    const int rigidbodyPower = 5;

    public SpriteRenderer entitySprite;
    public int life = 100;

    protected bool onDamage;
    protected float damageTime = 0.2f;

    protected void Update()
    {
        YPositionCorrection();
    }

    protected virtual void YPositionCorrection() {
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

    public void HaveHitADamage(int damageReceived, GameObject causerObject)
    {
        if (onDamage)
        {
            return;
        }

        life -= damageReceived;

        WhenTakingDamage(causerObject);
        StartCoroutine(DamageTimer());

        if (life <= 0)
        {
            WhenDying();
        }
    }

    protected virtual void WhenTakingDamage(GameObject causerObject) {}

    protected virtual IEnumerator DamageTimer()
    {
        onDamage = true;
        entitySprite.color = new Color(0.5f, 0.5f, 0.5f, 1f);

        yield return new WaitForSeconds(damageTime);

        entitySprite.color = new Color(1f, 1f, 1f, 1f);
        onDamage = false;
    }

    void PushScenery(GameObject scenery)
    {
        Rigidbody sceneryRigidbody = scenery.gameObject.GetComponent<Rigidbody>();

        Vector3 direction = scenery.gameObject.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();

        sceneryRigidbody.AddForceAtPosition(
            direction * rigidbodyPower,
            transform.position,
            ForceMode.Impulse
        );
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag == BREAKABLE_SCENERY_TAG) PushScenery(hit.gameObject);
    }
}
