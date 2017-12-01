using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    void Update()
    {
        this.transform.Rotate(Vector3.up * (Time.deltaTime * 20));
        this.transform.position = new Vector3(this.transform.position.x, PingPong(Time.time * 0.15f, 0.4f, 1.5f), this.transform.position.z);
    }

    float PingPong(float aValue, float aMin, float aMax)
    {
        return Mathf.PingPong(aValue, aMax - aMin) + aMin;
    }
}
