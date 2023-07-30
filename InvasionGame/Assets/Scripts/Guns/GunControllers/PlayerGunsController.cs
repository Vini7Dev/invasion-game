using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerGunsController : GunsController
{
    public TextMeshProUGUI bulletsInfoHud;

    void Update()
    {
        UpdateBulletsInfo();
    }

    void UpdateBulletsInfo()
    {
        if (currentFireGun)
        {
            if (currentFireGun.bullets > 0)
            {
                bulletsInfoHud.text = currentFireGun.bullets.ToString()
                    + "/"
                    + currentFireGun.maxBullets.ToString();
            }
            else
            {
                bulletsInfoHud.text = "Reloading...";
            }
        }
        else
        {
            bulletsInfoHud.text = "";
        }
    }
}
