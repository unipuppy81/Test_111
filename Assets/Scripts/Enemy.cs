using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float health = 50;
    [SerializeField] private float maxHealth;

    public float bleedDuration;
    public bool isBleeding = false;
    public bool isExplode = false;
    public float explosionRadius;
    private Coroutine bleedingCoroutine;
    [SerializeField] private GameObject particle;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    private float initialX;

    public bool isDead() { return health <= 0; }

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigid;
    private Color color;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        rigid.bodyType = RigidbodyType2D.Kinematic;
        color = spriteRenderer.color;
    }

    private void Start()
    {
        initialX = transform.position.x;
        maxHealth = 50.0f;
        health = maxHealth;
        isBleeding = false;
        isExplode = false;
        explosionRadius = 1.0f;
    }

    private void Update()
    {
        if (isDead())
        {
            Dead();
        }
        else
        {
            MoveDown();
        }

    }

    private void OnEnable()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public void StateSet(int i)
    {
        if (i == 1)
        {
            isBleeding = false;
            isExplode = false;
            explosionRadius = 1.0f;
        }
        else if (i == 3)
        {
            isBleeding = false;
            isExplode = true;
            explosionRadius = 1.0f;
        }
        else if(i == 5)
        {
            isBleeding = false;
            isExplode = true;
            explosionRadius = 2.0f;
        }
        else if(i == 7)
        {
            isBleeding = true;
            isExplode = true;
            explosionRadius = 2.0f;
        }
    }

    private void MoveDown()
    {
        Vector3 currentPosition = transform.position;

        if (GameManager.Instance.isCrush)
        {
            rigid.velocity = Vector2.zero;
            speed = 0f;
            transform.position = currentPosition;
        }
        else
        {
            speed = 3f;
            rigid.velocity = new Vector2(0, -speed);
            transform.position = new Vector3(initialX, currentPosition.y, currentPosition.z);
        }
    }

    public void OnHit(float damage)
    {
        Health -= damage;
        spriteRenderer.color = new Color(color.r, color.g, color.b, 0.5f);
        Invoke("ReturnSprite", 0.1f);
    }

    private void ReturnSprite()
    {
        spriteRenderer.color = new Color(color.r, color.g, color.b, 1.0f);
    }

    #region »ç¸Á ·ÎÁ÷
    private void Dead()
    {
        StartCoroutine(FlyAwayAndDestroy());
    }

    private IEnumerator FlyAwayAndDestroy()
    {
        float flySpeed = 0.1f;
        float duration = 0.5f;
        Vector2 targetPosition = new Vector2(transform.position.x, transform.position.y + 0.5f);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, flySpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;

            if ((Vector2)transform.position == targetPosition)
                break;

            yield return null;
        }

        if (isExplode)
            Explode();

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    private void Explode()
    {
        float explosionDamage = 0.001f;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<Enemy>().OnHit(explosionDamage);
            }
        }

        for(int i =0;i < 1; i++)
        {
            Instantiate(particle, transform.position, transform.rotation);
        }

    }

    private IEnumerator ApplyBleedingDamage(float bleedDamage, float interval)
    {
        bleedDuration = 3; 
        float elapsedTime = 0f;


        while (elapsedTime < bleedDuration)
        {
            health -= bleedDamage;

            if (health <= 0)
            {
                Dead();
                yield break; 
            }

            elapsedTime += interval;
            yield return new WaitForSeconds(interval);
        }

        isBleeding = false; 
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
            Destroy(gameObject);
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            //OnHit(collision.GetComponent<Bullet>().damage);
        }
    }

    
}
