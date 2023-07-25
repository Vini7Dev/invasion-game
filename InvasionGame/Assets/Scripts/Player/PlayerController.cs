using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Slider liveBarSlider;
    public GameObject liveBarSliderFillArea;

    int life = 100;

    void Update()
    {
        UpdateLifeBar();
    }

    void UpdateLifeBar()
    {
        liveBarSlider.value = life;
    }

    public void HaveHitADamage(int damageReceived)
    {
        life -= damageReceived;

        gameObject.SetActive(life > 0);
        liveBarSliderFillArea.SetActive(life > 0);
    }

    public bool IsAlive()
    {
        return life > 0;
    }
}
