using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Color floorColor;
    public Color wallColor;

    public Material floorMaterial;
    public Material wallMaterial;

    void Start()
    {
        floorMaterial.color = floorColor;
        wallMaterial.color = wallColor;
    }
}
