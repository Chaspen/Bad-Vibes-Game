using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Explosion : MonoBehaviour
{

    [HideInInspector]
    public AudioClip explosionSound;

    AudioSource source;

    float lifespan;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        source.PlayOneShot(explosionSound, 0.6f);
    }

    void Update()
    {
        lifespan += Time.deltaTime;

        if (lifespan > 1f)
            Destroy(this.gameObject);
    }
}
