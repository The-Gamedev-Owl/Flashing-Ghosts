using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashGauge : MonoBehaviour
{
    public GameObject player;

    private Image gaugeImage;

    private void Start()
    {
        gaugeImage = GetComponent<Image>();
    }

    void Update()
    {
        Vector3 newPos = player.transform.position;
        Color actualColor = gaugeImage.color;

        newPos.y -= 0.3f;
        newPos.x -= 0.3f;
        transform.position = Camera.main.WorldToScreenPoint(newPos);
        if (gaugeImage.fillAmount == 1 && gaugeImage.color.a != 0.5f)
            actualColor.a = 0.5f;
        else if (gaugeImage.fillAmount != 1 && gaugeImage.color.a != 1f)
            actualColor.a = 1f;
        gaugeImage.color = actualColor;
    }
}
