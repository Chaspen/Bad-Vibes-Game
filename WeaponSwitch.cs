using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    GameManager gameManager;

    public List<Transform> weapons;
    public int initialWeapon;
    public bool autoFill;

    public int selectedWeapon;

    private void Awake()
    {
        if (autoFill)
        {
            weapons.Clear();
            foreach (Transform weapon in transform)
            {
                weapons.Add(weapon);
            }
        }

        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        selectedWeapon = initialWeapon % weapons.Count;
        UpdateWeapon();
    }

    void Update()
    {
        if (gameManager.isDead == false)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                selectedWeapon = (selectedWeapon + 1) % weapons.Count;
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
                selectedWeapon = Mathf.Abs(selectedWeapon - 1) % weapons.Count;

            if (Input.GetKeyDown(KeyCode.Alpha1))
                selectedWeapon = 0;
            if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 1)
                selectedWeapon = 1;
            if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Count > 2)
                selectedWeapon = 2;

            UpdateWeapon();
        }
    }

    void UpdateWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == selectedWeapon)
                weapons[i].gameObject.SetActive(true);

            else
                weapons[i].gameObject.SetActive(false);
        }
    }
}
