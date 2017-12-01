using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MegaHands : MonoBehaviour
{

    public GameObject rocket;
    public GameObject explosion;
    public GameObject spawnPoint;

    public Sprite idleHands;
    public Sprite shootHands;

    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptySound;
    public AudioClip explosionSound;

    public float rocketForce;
    public float explosionRadius;
    public float explosionDamage;
    public LayerMask explosionMask;

    public Text ammoText;

    public int rocketsAmount;
    public int rocketsLeft;
    int maxRockets = 4;
    int rocketLoaded;
    AudioSource source;
    GameManager gameManager;

    bool isReloading;
    bool isCharged;
    bool isShot;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        rocketsLeft = rocketsAmount;
        gameManager = FindObjectOfType<GameManager>();

        rocketsLeft = gameManager.violetAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        rocketLoaded = isCharged ? 1 : 0;
        ammoText.text = "ENERGY: " + rocketLoaded + " / " + rocketsLeft;

        if (Input.GetButtonDown("Fire1") && isCharged && !isReloading && gameManager.isDead == false)
        {
            isCharged = false;
            StartCoroutine(Shoot());
            source.PlayOneShot(shotSound, 0.5f);
            GameObject rocketInstance = (GameObject)Instantiate(rocket, spawnPoint.transform.position, Quaternion.identity);
            rocketInstance.GetComponent<Rocket>().damage = explosionDamage;
            rocketInstance.GetComponent<Rocket>().radius = explosionRadius;
            rocketInstance.GetComponent<Rocket>().explosionSound = explosionSound;
            rocketInstance.GetComponent<Rocket>().layerMask = explosionMask;
            rocketInstance.GetComponent<Rocket>().explosion = explosion;
            Rigidbody rocketRB = rocketInstance.GetComponent<Rigidbody>();
            rocketRB.AddForce(Camera.main.transform.forward * rocketForce, ForceMode.Impulse);
            Reload();
        }
        else if (Input.GetButtonDown("Fire2") && !isCharged && !isReloading && gameManager.isDead == false ||
                 Input.GetKeyDown(KeyCode.R) && !isCharged && !isReloading && gameManager.isDead == false)
        {
            Reload();
        }

        if (rocketsLeft > maxRockets)
        {
            rocketsLeft = maxRockets;
        }
    }

    void Reload()
    {
        if (rocketsLeft <= 0)
        {
            source.PlayOneShot(emptySound, 0.4f);
        }
        else
        {
            StartCoroutine(ReupAmmo());
            rocketsLeft--;
            isCharged = true;
        }
    }

    public void AmmoCollect(int rocketsUp)
    {
        if (rocketsLeft <= maxRockets - rocketsUp)
        {
            rocketsLeft += rocketsUp;

        }
        else if (rocketsLeft > maxRockets - rocketsUp)
        {
            rocketsLeft = maxRockets;
        }
    }

    IEnumerator ReupAmmo()
    {
        isReloading = true;
        source.PlayOneShot(reloadSound, 0.6f);

        yield return new WaitForSeconds(1f);

        isReloading = false;
    }

    IEnumerator Shoot()
    {
        GetComponent<SpriteRenderer>().sprite = shootHands;
        yield return new WaitForSeconds(0.75f);
        GetComponent<SpriteRenderer>().sprite = idleHands;
    }
}
