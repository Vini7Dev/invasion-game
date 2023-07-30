using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    public bool open;

    float transitionSpeed = 10;
    Color transitionColor;
    Image transitionImage;

    void Start()
    {
        transitionImage = gameObject.GetComponent<Image>();
        transitionColor = new Color(0f, 0f, 0f, 1f);
    }

    void Update()
    {
        if (open)
        {
            OpenTransition();
        }
        else
        {
            CloseTransition();
        }
    }

    void CloseTransition()
    {
        if (transitionColor.a < 1)
        {
            transitionColor.a += Time.deltaTime * transitionSpeed;
            transitionImage.color = transitionColor;
        }
    }

    void OpenTransition()
    {
        if (transitionColor.a > 0)
        {
            transitionColor.a -= Time.deltaTime * transitionSpeed;
            transitionImage.color = transitionColor;
        }
    }
}
