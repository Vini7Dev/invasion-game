using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    const int rigidbodyPower = 5;

    public SpriteRenderer entitySprite;
    public GameObject audioSourceUtil;
    public AudioClip entityHitSound, dieSound;
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

    public void PlaySound(
        AudioClip soundClip,
        float timeToDestroy = 5,
        bool inGlobalParent = false
    )
    {
        if (audioSourceUtil == null) return;

        GameObject audioInstance = Instantiate(
            audioSourceUtil,
            audioSourceUtil.transform.position,
            audioSourceUtil.transform.rotation
        );

        if (!inGlobalParent) audioInstance.transform.parent = transform;

        AudioSourceUtil audioSource = audioInstance.GetComponent<AudioSourceUtil>();
        audioSource.PlaySound(soundClip, timeToDestroy);
    }

    public void HaveHitADamage(int damageReceived, GameObject causerObject)
    {
        if (onDamage || !enabled) return;

        life -= damageReceived;

        PlaySound(entityHitSound, 2, true);
        WhenTakingDamage(causerObject);
        StartCoroutine(DamageTimer());

        if (life <= 0)
        {
            PlaySound(dieSound, 5, true);
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

    void PushDynamicScenery(GameObject scenery)
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
        if (hit.gameObject.tag == GlobalTags.BREAKABLE_SCENERY) PushDynamicScenery(hit.gameObject);
    }
}
