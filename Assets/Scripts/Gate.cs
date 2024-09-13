using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private float speed = 3.0f;

    void Update()
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
            transform.position = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z);
        }
    }
}
