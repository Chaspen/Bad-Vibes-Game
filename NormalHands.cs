using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Collections.Specialized;

[RequireComponent(typeof(AudioSource))]

public class NormalHands : MonoBehaviour
{
    public GameObject bloodSplatter;
    public Sprite idleHands;
    public Sprite shootHands;
    public float normalDamage;
    public float normalRange;
    public float fireRate;
    public int ammoCount;
    public int ammoLoadSize;
    public AudioClip shotSound;
    public AudioClip reload;
    public AudioClip empty;
    public Text ammoText;
    public GameObject[] hitPoints;
    public GameObject shotAnim;

    public int ammoLeft;
    public int maxAmmo;
    int ammoLoadLeft;
    float nextFire;

    bool isShot;
    bool isReloading;

    AudioSource source;
    GameManager gameManager;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();

        if (this.gameObject.name == "Normal_Hands")
        {
            ammoLeft = gameManager.pinkAmmo;
        }
        else if (this.gameObject.name == "Fast_Hands")
        {
            ammoLeft = gameManager.blueAmmo;
        }

        ammoLoadLeft = ammoLoadSize;
        shotAnim.SetActive(false);
    }

    private void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        ammoText.text = "ENERGY: " + ammoLoadLeft + " / " + ammoLeft;

        if (Input.GetButton("Fire1") && isReloading == false && Time.time >= nextFire && gameManager.isDead == false)
        {
            isShot = true;
            nextFire = Time.time + (1f / fireRate);
        }

        if (Input.GetKeyDown(KeyCode.R) && isReloading == false || Input.GetButton("Fire2") && isReloading == false && gameManager.isDead == false)
        {
            Reload();
        }

        if (ammoLeft > maxAmmo)
        {
            ammoLeft = maxAmmo;
        }
    }

    void FixedUpdate()
    {
        Vector2 shotOffset = Random.insideUnitCircle * DynamicCrosshair.spread;
        Vector3 randomTarget = new Vector3(Screen.width / 2 + shotOffset.x, Screen.height / 2 + shotOffset.y, 0);
        Ray ray = Camera.main.ScreenPointToRay(randomTarget);
        RaycastHit hit;
        if (isShot == true && ammoLoadLeft > 0 && isReloading == false && gameManager.isDead == false)
        {
            isShot = false;
            DynamicCrosshair.spread += DynamicCrosshair.SHOOT_SPREAD;
            ammoLoadLeft--;
            source.PlayOneShot(shotSound, 0.75f);
            StartCoroutine(Shoot());
            if (Physics.Raycast(ray, out hit, normalRange))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Instantiate(bloodSplatter, hit.point, Quaternion.identity);

                    if (hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().patrolState ||
                        hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().alertState)
                    {
                        hit.collider.gameObject.SendMessage("HiddenShot", transform.parent.transform.position, SendMessageOptions.DontRequireReceiver);
                    }

                    hit.collider.gameObject.SendMessage("AddDamage", normalDamage, SendMessageOptions.DontRequireReceiver);

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(Camera.main.transform.forward * 1, ForceMode.Impulse);
                    }
                }
                else if (hit.transform.CompareTag("Missile"))
                {
                    Destroy(hit.collider.gameObject);
                }
                else if (hit.transform.CompareTag("Scenery"))
                {
                    GameObject hitInstance = hitPoints[Random.Range(0, hitPoints.Length)];
                    Instantiate(hitInstance, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.parent = hit.collider.gameObject.transform;
                }
            }
        }
        else if (isShot == true && ammoLoadLeft <= 0 && isReloading == false && gameManager.isDead == false)
        {
            isShot = false;
            Reload();
        }
    }

    void Reload()
    {
        int ammoToReload = ammoLoadSize - ammoLoadLeft;

        if (ammoLeft >= ammoToReload)
        {
            StartCoroutine(ReupAmmo());
            ammoLeft -= ammoToReload;
            ammoLoadLeft = ammoLoadSize;
        }
        else if (ammoLeft < ammoToReload && ammoLeft > 0)
        {
            StartCoroutine(ReupAmmo());
            ammoLoadLeft += ammoLeft;
            ammoLeft = 0;
        }
        else if (ammoLeft <= 0)
        {
            source.PlayOneShot(empty, 0.4f);
        }
    }

    public void AmmoCollect(int ammoUp)
    {
        if (ammoLeft <= maxAmmo - ammoUp)
        {
            ammoLeft += ammoUp;

        }
        else if (ammoLeft > maxAmmo - ammoUp)
        {
            ammoLeft = maxAmmo;
        }
    }

    IEnumerator ReupAmmo()
    {
        isReloading = true;
        source.PlayOneShot(reload, 0.6f);
        yield return new WaitForSeconds(0.5f);
        isReloading = false;
    }

    IEnumerator Shoot()
    {
        if (this.gameObject.name == "Normal_Hands")
        {
            GetComponent<SpriteRenderer>().sprite = shootHands;
            shotAnim.SetActive(true);
            yield return new WaitForSeconds(0.75f);
            shotAnim.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = idleHands;
        }

        if (this.gameObject.name == "Fast_Hands")
        {
            GetComponent<SpriteRenderer>().sprite = shootHands;
            shotAnim.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            shotAnim.SetActive(false);
            GetComponent<SpriteRenderer>().sprite = idleHands;
        }
    }
}
