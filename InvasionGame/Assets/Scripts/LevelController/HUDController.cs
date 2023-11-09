using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LifeBarController
{
    public Slider lifeBarSlider;
    public Image lifeBarSliderFill;

    Color greenColor = new Color(0.2f, 1f, 0f, 0.4f);
    Color yellowColor = new Color(1f, 1f, 0f, 0.4f);
    Color redColor = new Color(1f, 0f, 0f, 0.4f);

    public void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        PlayerController playerController = playerObject.GetComponent<PlayerController>();

        UpdateLifeBar(playerController.life);
    }

    Color GetLifeBarColor(int updatedLife)
    {
        if (updatedLife > 66) return greenColor;
        else if (updatedLife > 33) return yellowColor;
        else return redColor;
    }

    public void UpdateLifeBar(int updatedLife)
    {
        lifeBarSlider.value = updatedLife;

        lifeBarSliderFill.color = GetLifeBarColor(updatedLife);
    }
}

public class HUDController : MonoBehaviour
{
    public LifeBarController lifeBarController;

    void Start()
    {
        lifeBarController.Start();
    }

    public void UpdateLifeBar(int updatedLife)
    {
        lifeBarController.UpdateLifeBar(updatedLife);
    }
}
