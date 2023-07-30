using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextParticle : MonoBehaviour
{
    public Color[] textColor = new Color[2];

    float fallingSpeed = 2.5f;
    TextMeshProUGUI damageTextMesh;

    void Start()
    {
        damageTextMesh = GetComponent<TextMeshProUGUI>();
        damageTextMesh = GetComponentInChildren<TextMeshProUGUI>();
        Destroy(gameObject, 1);
    }
    
    void Update()
    {
        FallingAnimation();
    }

    public void SetupDamageText(
        int damageValue,
        int damageColorIndex
    )
    {
        damageTextMesh.text = damageValue.ToString();
        damageTextMesh.color = textColor[damageColorIndex];
    }

    void FallingAnimation()
    {
        transform.position -= Vector3.forward * Time.deltaTime * fallingSpeed;
    }
}
