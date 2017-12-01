using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public bool spawned;
    public float timer;
    public float lerpTimer;

    Light light;
    float normalIntensity = 2f;
    float spawnIntensity = 7f;
    float lerpTime = 1f;

    Vector3 offset = new Vector3(0.5f, 0, 0.5f);

    void Awake()
    {
        light = GetComponentInChildren<Light>();
    }

    void Start()
    {
        Instantiate(enemy, this.transform.position + offset, Quaternion.identity);
    }

    void Update()
    {
        timer += Time.deltaTime;
        lerpTimer += Time.deltaTime;

        float lerp = lerpTimer / lerpTime;

        if (lerp > 0.99f)
        {
            lerpTimer = 0;
        }

        if (spawned == true && timer <= 1f)
        {
            light.range = Mathf.Lerp(normalIntensity, spawnIntensity, lerp);
        }

        if (spawned == true && timer > 1f)
        {
            light.range = Mathf.Lerp(spawnIntensity, normalIntensity, lerp);
        }

        if (spawned == true && timer > 2f)
        {
            spawned = false;
        }

        if (spawned == false)
        {
            timer = 0;
            light.range = normalIntensity;
        }
    }
}
