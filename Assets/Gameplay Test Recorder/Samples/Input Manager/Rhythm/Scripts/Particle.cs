using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float lifetime = 1;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
