using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashScreen : MonoBehaviour
{
    public bool gameOverScreen = false;

    Image flashScreen;

    void Start()
    {
        flashScreen = GetComponent<Image>();
    }

    void Update()
    {
        if (flashScreen.color.a > 0 && gameOverScreen == false)
        {
            Color invisible = new Color(flashScreen.color.r, flashScreen.color.g, flashScreen.color.b, 0);
            flashScreen.color = Color.Lerp(flashScreen.color, invisible, 5 * Time.deltaTime);
        }
        else if (flashScreen.color.a > 0 && gameOverScreen == true)
        {
            Color white = new Color(flashScreen.color.r, flashScreen.color.g, flashScreen.color.b, 1);
            flashScreen.color = Color.Lerp(flashScreen.color, white, 0.6f * Time.deltaTime);
        }

    }

    public void TookDamage()
    {
        flashScreen.color = new Color(1, 1, 1, 0.7f);
    }
}
