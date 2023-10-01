using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReloadingBar : MonoBehaviour
{
    float timeToEnd, timer;
    Vector3 fullBarSize = new Vector3(0.15f, 0.03f, 0.03f);

    void Update()
    {
        DecreaseBarAndTimer();
    }

    void DecreaseBarAndTimer() {
        if (timer >= timeToEnd) {
            transform.localScale = Vector3.zero;
            return;
        }

        timer += Time.deltaTime;

        float barProgressSize = fullBarSize.x * timer / timeToEnd;

        transform.localScale = new Vector3(
            barProgressSize,
            fullBarSize.y,
            fullBarSize.y
        );
    }

    public void StartBar(float setTimeToEnd) {
        timer = 0.01f;
        timeToEnd = setTimeToEnd;
        transform.localScale = fullBarSize;
    }
}
