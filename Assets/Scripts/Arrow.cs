using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int life = 1;
    public float damage = 0;
    public float destroyTime = 5f; 

    private void Start()
    { 
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        if (life <= 0)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().OnHit(damage);
            life--;
        }
    }
}
