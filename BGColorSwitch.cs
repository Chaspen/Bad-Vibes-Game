using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGColorSwitch : MonoBehaviour
{
    public bool dimmed;

    WeaponSwitch weaponSwitch;
    Camera cam;

    Color currentColor;
    Color nextColor;

    Color color1;
    Color color2;
    Color color3;

    void Start()
    {

        weaponSwitch = FindObjectOfType<WeaponSwitch>();
        cam = GetComponent<Camera>();

        currentColor = cam.backgroundColor;
        nextColor = currentColor;

    }

    void Update()
    {
        if (!dimmed)
        {
            color1 = new Color(1, 0.21f, 0.12f, 0);
            color2 = new Color(0.23f, 0.54f, 0.98f, 0);
            color3 = new Color(0.65f, 0.15f, 1f, 0);

            if (weaponSwitch.selectedWeapon == 0)
            {
                nextColor = color1;
                currentColor = cam.backgroundColor;
                cam.backgroundColor = Color.Lerp(currentColor, nextColor, Time.deltaTime * 1f);
            }
            else if (weaponSwitch.selectedWeapon == 1)
            {
                nextColor = color2;
                currentColor = cam.backgroundColor;
                cam.backgroundColor = Color.Lerp(currentColor, nextColor, Time.deltaTime * 1f);
            }
            else if (weaponSwitch.selectedWeapon == 2)
            {
                nextColor = color3;
                currentColor = cam.backgroundColor;
                cam.backgroundColor = Color.Lerp(currentColor, nextColor, Time.deltaTime * 1f);
            }
        }
        else if (dimmed)
        {
            color1 = new Color(0.6f, 0.01f, 0.02f, 0);
            color2 = new Color(0.03f, 0.24f, 0.68f, 0);
            color3 = new Color(0.1f, 0f, 0.3f, 0);

            if (weaponSwitch.selectedWeapon == 0)
            {
                nextColor = color1;
                currentColor = cam.backgroundColor;
                cam.backgroundColor = Color.Lerp(currentColor, nextColor, Time.deltaTime * 1f);
            }
            else if (weaponSwitch.selectedWeapon == 1)
            {
                nextColor = color2;
                currentColor = cam.backgroundColor;
                cam.backgroundColor = Color.Lerp(currentColor, nextColor, Time.deltaTime * 1f);
            }
            else if (weaponSwitch.selectedWeapon == 2)
            {
                nextColor = color3;
                currentColor = cam.backgroundColor;
                cam.backgroundColor = Color.Lerp(currentColor, nextColor, Time.deltaTime * 1f);
            }
        }
    }

}
