using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splatter : MonoBehaviour
{
    SpriteRenderer sprite;

    float timer;

    void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        Color startColor = sprite.color;
        Color endColor = new Color(1, 1, 1, 0);

        sprite.color = Color.Lerp(startColor, endColor, timer * 0.007f);

        if (timer >= 30)
        {
            Destroy(this.gameObject);
        }

    }

}
