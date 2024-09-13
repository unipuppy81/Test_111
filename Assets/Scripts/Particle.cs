using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    void Start()
    {
        Invoke("Die", 0.2f);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
