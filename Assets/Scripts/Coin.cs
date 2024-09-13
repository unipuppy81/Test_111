using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    private float speed;
    private float initialX;
    private void Start()
    {
        initialX = transform.position.x;
        speed = 1.0f;
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
        }
        else
        {
            speed = 3f;
            rigid.velocity = new Vector2(0, -speed);
            transform.position = new Vector3(initialX, currentPosition.y, currentPosition.z);
        }
    }
}
