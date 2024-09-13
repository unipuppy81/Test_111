using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public GameObject player;    
    public GameObject arrowPrefab; 
    public float followSpeed = 5f; 
    public float fireRate = 1f;    
    public Transform firePoint;
    public int hunterLevel = 1;

    private float fixedYPosition;  
    private Coroutine fireCoroutine;
    public int arrowLife = 1;
    public float hunterDamage = 0;
    private int arrowCount = 0;
    public bool doubleShot = false;

    private void Start()
    {
        fixedYPosition = transform.position.y;
        firePoint.position = new Vector3(firePoint.position.x, firePoint.position.y + 2, firePoint.position.z);
        fireCoroutine = StartCoroutine(FireArrowRoutine());
    }

    private void Update()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, fixedYPosition, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    private IEnumerator FireArrowRoutine()
    {
        while (true)
        {
            if (hunterLevel >= 3)
            {
                if (doubleShot)
                {
                    FireArrow();
                    yield return new WaitForSeconds(0.1f); 
                    FireArrow();
                }
                else
                {
                    FireArrow();
                }

                arrowCount++;

                if (arrowCount >= 5 && !doubleShot)
                {
                    doubleShot = true;
                }
            }
            else
            {
                FireArrow();
            }


            yield return new WaitForSeconds(fireRate);
        }
    }

    private void FireArrow()
    {
        GameObject arrow = PoolManager.Instance.SpawnFromPool(GameManager.ARROW, firePoint.position, Quaternion.identity);
        Arrow a = arrow.GetComponent<Arrow>();
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        a.life = arrowLife;
        a.damage = hunterDamage;
        rb.velocity = Vector2.up * 10f;
    }
    public void SetFireRate(float newFireRate)
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
        fireRate = newFireRate;
        fireCoroutine = StartCoroutine(FireArrowRoutine());
    }

    public void ResetArrowCount()
    {
        arrowCount = 0;
        doubleShot = false;
    }

    public void SetHunterLevel(int newLevel)
    {
        hunterLevel = newLevel;
        ResetArrowCount();
    }
}
