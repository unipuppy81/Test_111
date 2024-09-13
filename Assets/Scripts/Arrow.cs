using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int life = 1;
    public float damage = 0;
    public float destroyTime = 5f; 


    private void Update()
    {
        if (life <= 0)
            PoolManager.Instance.ReturnToPool(GameManager.ARROW, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().OnHit(damage);
            life--;
        }
        else if (collision.CompareTag("Border"))
        {
            PoolManager.Instance.ReturnToPool(GameManager.ARROW, gameObject);
        }
        
    }
}
