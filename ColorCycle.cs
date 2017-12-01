using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorCycle : MonoBehaviour
{
    float timer;
    Text title;

    Color currentColor;
    Color nextColor;

    Color color1 = new Color(1, 0.21f, 0.12f, 1);
    Color color2 = new Color(0.23f, 0.54f, 0.98f, 1);
    Color color3 = new Color(0.65f, 0.15f, 1f, 1);

    void Start()
    {

        title = GetComponent<Text>();

        currentColor = new Color(1, 1, 1, 0);
        nextColor = currentColor;

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 3)
        {
            nextColor = color1;
            currentColor = title.color;
            title.color = Color.Lerp(currentColor, nextColor, Time.deltaTime * 0.75f);
        }
        else if (timer >= 3 && timer < 6)
        {
            nextColor = color2;
            currentColor = title.color;
            title.color = Color.Lerp(currentColor, nextColor, Time.deltaTime * 0.75f);
        }
        else if (timer >= 6 && timer < 9)
        {
            nextColor = color3;
            currentColor = title.color;
            title.color = Color.Lerp(currentColor, nextColor, Time.deltaTime * 0.75f);
        }
        else if (timer >= 9 && timer < 12)
        {
            nextColor = new Color(1, 1, 1, 1);
            currentColor = title.color;
            title.color = Color.Lerp(currentColor, nextColor, Time.deltaTime * 0.75f);
        }
        else if (timer >= 12)
        {
            timer = 0;
        }
    }
}
