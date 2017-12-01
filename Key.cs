using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    PlayerHealth playerHealth;
    GameManager gameManager;

    bool isCollected = false;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        gameManager = FindObjectOfType<GameManager>();
    }


    void Update()
    {

        this.transform.Rotate(Vector3.up * (Time.deltaTime * 20));

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isCollected == false)
        {
            playerHealth.health = playerHealth.maxHealth;
            gameManager.DestroySpawner();
            gameManager.KeyHUD();
            gameManager.AddPoints(25);
            isCollected = true;
            CheckKey();
            Destroy(this.gameObject, 0.25f);
        }
    }

    void CheckKey()
    {
        if (this.gameObject.name == "AlienKey(Clone)")
        {
            gameManager.alienKey = true;
        }
        else if (this.gameObject.name == "PizzaKey(Clone)")
        {
            gameManager.pizzaKey = true;
        }
        else if (this.gameObject.name == "SmileyKey(Clone)")
        {
            gameManager.smileyKey = true;
        }
    }

}
