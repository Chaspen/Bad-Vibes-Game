using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    GameManager gameManager;

    public bool squashed = false;
    public float health;
    public int maxHealth;
    public AudioClip[] hits;
    public AudioClip death;
    public AudioClip keyCollect;
    public AudioClip itemCollect;
    public FlashScreen flash;

    AudioSource source;
    Text healthUI;
    Camera weaponCam;

    float shakeDuration;
    float shakeMagnitude;
    float deathTimer;

    void Awake()
    {

        health = maxHealth;
        healthUI = GameObject.Find("HealthUI").GetComponent<Text>();
        source = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        weaponCam = GameObject.Find("WeaponCamera").GetComponent<Camera>();

        shakeDuration = 0.15f;
        shakeMagnitude = 0.3f;

    }

    void Update()
    {
        if (health > 0)
        {
            healthUI.text = "VIBES: " + health.ToString();
        }

        if (health <= 0 && gameManager.isDead == false && gameManager.gameWon == false)
        {
            gameManager.isDead = true;
            healthUI.text = "VIBES: BAD";
        }

        if (squashed)
        {
            deathTimer += Time.deltaTime;
        }
        else
            deathTimer = 0;

        if (squashed && deathTimer >= 2 && gameManager.isDead == false)
        {
            gameManager.isDead = true;
            source.PlayOneShot(death, 1);
            health = 0;
            healthUI.text = "VIBES: BAD";
            flash.TookDamage();
        }

    }

    void EnemyHit(float damage)
    {
        if (health - damage > 0 && gameManager.gameWon == false)
        {
            AudioClip hit = hits[Random.Range(0, hits.Length)];

            source.PlayOneShot(hit, 0.8f);
            health -= damage;
            flash.TookDamage();

            StartCoroutine(CameraShake());
        }
        else if (health - damage <= 0 && gameManager.gameWon == false)
        {
            source.PlayOneShot(death, 1);
            health -= damage;
            flash.TookDamage();

            StartCoroutine(CameraShake());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Key")
        {
            source.PlayOneShot(keyCollect, 0.75f);
        }
        else if (other.tag == "Loot" && gameManager.gameWon == false)
        {
            source.PlayOneShot(itemCollect, 0.5f);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Platform" && gameManager.gameWon == false)
        {
            squashed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Platform" && gameManager.gameWon == false)
        {
            squashed = false;
        }
    }

    public void AddHealth(float healthUp)
    {
        if (health <= maxHealth - healthUp)
        {
            health += healthUp;
        }
        else if (health > maxHealth - healthUp)
        {
            health = maxHealth;
        }
    }

    IEnumerator CameraShake()
    {

        float elapsed = 0.0f;

        Vector3 originalCamPos = weaponCam.transform.position;

        while (elapsed < shakeDuration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / shakeDuration;
            float damper = 1f - Mathf.Clamp(2f * percentComplete - 0f, 0f, 0f);

            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= shakeMagnitude * damper;
            y *= shakeMagnitude * damper;

            Camera.main.transform.position = new Vector3((x + originalCamPos.x), (y + originalCamPos.y), originalCamPos.z);

            yield return null;
        }

        Camera.main.transform.position = weaponCam.transform.position;

    }

}
