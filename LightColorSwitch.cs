using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorSwitch : MonoBehaviour
{

    WeaponSwitch weaponSwitch;
    Light light;

    Color currentColor;
    Color nextColor;

    public bool dimmed = false;

    Color color1;
    Color color2;
    Color color3;

    void Start()
    {

        weaponSwitch = FindObjectOfType<WeaponSwitch>();
        light = GetComponent<Light>();

        currentColor = light.color;
        nextColor = currentColor;

    }

    void Update()
    {
        if (!dimmed)
        {
            color1 = new Color(0.41f, 0.61f, 0.76f, 0);
            color2 = new Color(0.52f, 0.98f, 0.87f, 0);
            color3 = new Color(1f, 0.65f, 0.87f, 0);

            if (weaponSwitch.selectedWeapon == 0)
            {
                nextColor = color1;
                currentColor = light.color;
                light.color = Color.Lerp(currentColor, nextColor, Time.deltaTime * 1f);
            }
            else if (weaponSwitch.selectedWeapon == 1)
            {
                nextColor = color2;
                currentColor = light.color;
                light.color = Color.Lerp(currentColor, nextColor, Time.deltaTime * 1f);
            }
            else if (weaponSwitch.selectedWeapon == 2)
            {
                nextColor = color3;
                currentColor = light.color;
                light.color = Color.Lerp(currentColor, nextColor, Time.deltaTime * 1f);
            }
        }
        else if (dimmed)
        {
            color1 = new Color(0.01f, 0.11f, 0.26f, 0);
            color2 = new Color(0.02f, 0.48f, 0.37f, 0);
            color3 = new Color(0.5f, 0.15f, 0.37f, 0);//new Color(0, 0.1f, 0.3f, 0);

            if (weaponSwitch.selectedWeapon == 0)
            {
                nextColor = color1;
                currentColor = light.color;
                light.color = Color.Lerp(currentColor, nextColor, Time.deltaTime * 1f);
            }
            else if (weaponSwitch.selectedWeapon == 1)
            {
                nextColor = color2;
                currentColor = light.color;
                light.color = Color.Lerp(currentColor, nextColor, Time.deltaTime * 1f);
            }
            else if (weaponSwitch.selectedWeapon == 2)
            {
                nextColor = color3;
                currentColor = light.color;
                light.color = Color.Lerp(currentColor, nextColor, Time.deltaTime * 1f);
            }

        }
    }

}
