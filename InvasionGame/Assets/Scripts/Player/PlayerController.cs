using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int life = 100;
    public Slider liveBarSlider;
    public GameObject liveBarSliderFillArea;
    public GameObject playerSpriteObject;

    float damageTime = 0.2f;
    bool onDamage;
    SpriteRenderer playerSprite;

    void Start()
    {
        playerSprite = playerSpriteObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateLifeBar();
    }

    void UpdateLifeBar()
    {
        liveBarSlider.value = life;
    }

    IEnumerator DamageTimer()
    {
        onDamage = true;
        playerSprite.color = new Color(0.5f, 0.5f, 0.5f, 1f);

        yield return new WaitForSeconds(damageTime);

        playerSprite.color = new Color(1f, 1f, 1f, 1f);
        onDamage = false;
    }

    public void HaveHitADamage(int damageReceived)
    {
        if (onDamage)
        {
            return;
        }

        life -= damageReceived;
        StartCoroutine(DamageTimer());

        gameObject.SetActive(life > 0);
        liveBarSliderFillArea.SetActive(life > 0);
    }

    public bool IsAlive()
    {
        return life > 0;
    }
}
