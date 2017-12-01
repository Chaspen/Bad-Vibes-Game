using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public AudioClip enemyHit;
    public AudioClip enemyDeath;
    public GameObject[] loot;

    float health;
    bool dead;

    GameManager gameManager;
    EnemyStates es;
    NavMeshAgent nma;
    BoxCollider bc;
    Animator anim;
    AudioSource audio;

    void Start()
    {
        health = maxHealth;
        es = GetComponent<EnemyStates>();
        nma = GetComponent<NavMeshAgent>();
        bc = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (health <= 0)
        {
            es.enabled = false;
            nma.enabled = false;
            bc.center = new Vector3(0f, -1.3f, 0f);
        }
    }

    void AddDamage(float damage)
    {
        health -= damage;

        if (health > 0)
        {
            StartCoroutine(HitAnim());
        }
        else if (health <= 0 && dead == false)
        {
            audio.PlayOneShot(enemyDeath, 0.6f);
            anim.Play("Enemy_Death");
            StartCoroutine(DropLoot());
            gameManager.SpawnEnemy();
            this.gameObject.tag = "Dead";
            dead = true;

            if (!gameManager.ending)
            {
                gameManager.AddPoints(10);
            }
            else if (gameManager.ending)
            {
                gameManager.AddPoints(20);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hazard") && health <= 0)
        {
            bc.enabled = false;
            Destroy(this.gameObject, 5f);
        }
    }

    IEnumerator DropLoot()
    {
        GameObject lootInstance = loot[Random.Range(0, loot.Length)];
        if (lootInstance.GetComponent<Ammo>() != null)
        {
            lootInstance.GetComponent<Ammo>().loot = true;
        }

        if (lootInstance.GetComponent<Health>() != null)
        {
            lootInstance.GetComponent<Health>().loot = true;
        }

        yield return new WaitForSeconds(0.75f);

        Instantiate(lootInstance, new Vector3(this.transform.position.x, 0.6f, this.transform.position.z), Quaternion.identity);
    }

    IEnumerator HitAnim()
    {
        anim.SetBool("isHit", true);

        yield return new WaitForSeconds(0.10f);

        anim.SetBool("isHit", false);

        yield return new WaitForSeconds(0.2f);

        audio.PlayOneShot(enemyHit, 0.9f);
    }

}
