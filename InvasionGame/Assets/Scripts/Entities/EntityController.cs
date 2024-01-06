using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    const int rigidbodyPower = 5;

    public SpriteRenderer entitySprite;
    public GameObject audioSourceUtil;
    public AudioClip entityHitSound;
    public int life = 100;

    protected bool onDamage;
    protected float damageTime = 0.2f;
    protected EntitySkills entitySkills;

    protected void Start()
    {
        entitySkills = GetComponent<EntitySkills>();

        if (!entitySkills) return;

        life = entitySkills.maxLife;
    }

    protected void Update()
    {
        YPositionCorrection();
    }

    protected virtual void YPositionCorrection()
    {
        Vector3 positionCorrect = new Vector3(
            transform.position.x,
            0.1f,
            transform.position.z
        );

        transform.position = positionCorrect;
    }

    protected void PlaySound(AudioClip soundClip, float timeToDestroy = 5)
    {
        if (audioSourceUtil == null) return;

        GameObject audioInstance = Instantiate(
            audioSourceUtil,
            audioSourceUtil.transform.position,
            audioSourceUtil.transform.rotation
        );

        AudioSourceUtil audioSource = audioInstance.GetComponent<AudioSourceUtil>();
        audioSource.PlaySound(soundClip, timeToDestroy);
    }

    protected virtual void WhenDying()
    {
        gameObject.SetActive(false);
    }

    public virtual void AddLife(int lifeToAdd)
    {
        if (life + lifeToAdd > entitySkills.maxLife)
        {
            life = entitySkills.maxLife;
        }
        else
        {
            life += lifeToAdd;
        }
    }

    public void HaveHitADamage(int damageReceived, GameObject causerObject)
    {
        if (onDamage || !enabled) return;

        life -= damageReceived;

        PlaySound(entityHitSound);
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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == GlobalTags.BREAKABLE_SCENERY) PushScenery(hit.gameObject);
    }
}
