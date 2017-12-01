using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTexture : MonoBehaviour
{
    public Material cross;
    public Material stone;
    public Material wall;

    MeshRenderer mesh;

    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        float num = Random.value;

        if (num < 0.03f)
        {
            mesh.material = cross;
        }
        else if (num >= 0.03f && num < 0.05f)
        {
            mesh.material = wall;
        }
        else if (num >= 0.05f)
        {
            mesh.material = stone;
        }
    }

}
