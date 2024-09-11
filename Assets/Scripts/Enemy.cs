using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }


    private float initialX;


    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigid;
    private Color color;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        rigid.bodyType = RigidbodyType2D.Kinematic;
        color = spriteRenderer.color;
        health = maxHealth;
    }

    private void Start()
    {
        initialX = transform.position.x;
    }

    private void Update()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        Vector3 currentPosition = transform.position;

        if (GameManager.Instance.isCrush)
        {
            rigid.velocity = Vector2.zero;
            speed = 0f;
            transform.position = currentPosition;
            Debug.Log(gameObject.name);
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
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ReturnSprite()
    {
        spriteRenderer.color = new Color(color.r, color.g, color.b, 1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
            Destroy(gameObject);
    }
}
