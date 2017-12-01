using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    GameManager gameManager;

    NormalHands normal;
    NormalHands fast;
    MegaHands mega;
    Rigidbody rb;
    PlayerHealth playerHealth;

    bool dropped = false;
    bool isCollected = false;
    public bool loot = false;

    int blueUp = 10;
    int pinkUp = 5;
    int violetUp = 2;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        fast = gameManager.fastHands.GetComponent<NormalHands>();
        normal = gameManager.normalHands.GetComponent<NormalHands>();
        mega = gameManager.megaHands.GetComponent<MegaHands>();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (this.gameObject.name != "SecretPU")
        {
            this.transform.position = new Vector3(this.transform.position.x, PingPong(Time.time * 0.25f, 0.25f, 0.75f), this.transform.position.z);
        }

        if (rb != null && loot == true && dropped == false)
        {
            rb.AddForce(-Camera.main.transform.forward * 10, ForceMode.Impulse);
            dropped = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isCollected == false)
        {
            if (this.gameObject.name == "BlueAmmo(Clone)")
            {
                fast.AmmoCollect(blueUp);
                isCollected = true;
                gameManager.BlueHUD();
                gameManager.AddPoints(5);
                Destroy(this.gameObject, 0.25f);
            }
            else if (this.gameObject.name == "PinkAmmo(Clone)")
            {
                normal.AmmoCollect(pinkUp);
                isCollected = true;
                gameManager.PinkHUD();
                gameManager.AddPoints(5);
                Destroy(this.gameObject, 0.25f);
            }
            else if (this.gameObject.name == "VioletAmmo(Clone)")
            {
                mega.AmmoCollect(violetUp);
                isCollected = true;
                gameManager.VioletHUD();
                gameManager.AddPoints(5);
                Destroy(this.gameObject, 0.25f);
            }
            else if (this.gameObject.name == "SecretPU")
            {
                fast.AmmoCollect(blueUp);
                normal.AmmoCollect(pinkUp);
                mega.AmmoCollect(violetUp);
                playerHealth.health = playerHealth.maxHealth;
                gameManager.SecretHUD();
                gameManager.AddPoints(50);
                isCollected = true;
                Destroy(this.gameObject, 0.25f);
            }
        }
    }

    float PingPong(float aValue, float aMin, float aMax)
    {
        return Mathf.PingPong(aValue, aMax - aMin) + aMin;
    }
}
