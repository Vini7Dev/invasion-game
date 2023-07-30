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

    float damageTimer, damageTime = 0.2f;
    SpriteRenderer playerSprite;

    void Start()
    {
        playerSprite = playerSpriteObject.GetComponent<SpriteRenderer>();
        damageTimer = damageTime;
    }

    void Update()
    {
        UpdateLifeBar();
        ReloadDamageTimer();
    }

    void UpdateLifeBar()
    {
        liveBarSlider.value = life;
    }

    void ReloadDamageTimer()
    {
        if (damageTimer <= damageTime)
        {
            playerSprite.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            damageTimer += Time.deltaTime;
        }
        else
        {
            playerSprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void HaveHitADamage(int damageReceived)
    {
        if (damageTimer < damageTime)
        {
            return;
        }
        
        damageTimer = 0;
        life -= damageReceived;

        gameObject.SetActive(life > 0);
        liveBarSliderFillArea.SetActive(life > 0);
    }

    public bool IsAlive()
    {
        return life > 0;
    }
}
