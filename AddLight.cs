using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLight : MonoBehaviour
{

    public GameObject[] ornaments;
    public Transform[] ornamentSpots;

    void Start()
    {
        float num = Random.value;

        if (num > 0.9f)
        {
            Transform spot = ornamentSpots[Random.Range(0, ornamentSpots.Length)];
            GameObject ornament = ornaments[Random.Range(0, ornaments.Length)];

            Instantiate(ornament, spot.position, spot.rotation, this.gameObject.transform);
        }
        else
            return;
    }

}
