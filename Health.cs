using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    PlayerHealth playerHealth;
    GameManager gameManager;
    Rigidbody rb;

    public float healthUp = 10f;

    bool dropped = false;
    bool isCollected = false;
    public bool loot = false;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        gameManager = FindObjectOfType<GameManager>();

    }

    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, PingPong(Time.time * 0.25f, 0.4f, 0.8f), this.transform.position.z);

        this.transform.Rotate(Vector3.up * (Time.deltaTime * 30));

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
            playerHealth.AddHealth(healthUp);
            gameManager.HealthHUD();
            gameManager.AddPoints(5);
            isCollected = true;
            Destroy(this.gameObject, 0.25f);
        }
    }

    float PingPong(float aValue, float aMin, float aMax)
    {
        return Mathf.PingPong(aValue, aMax - aMin) + aMin;
    }

}
