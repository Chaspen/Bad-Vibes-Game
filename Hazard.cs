using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    float timer;
    float damage = 5f;

    void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && timer >= 5)
        {
            other.SendMessage("EnemyHit", damage, SendMessageOptions.DontRequireReceiver);
            timer = 0;
        }
    }
}
