using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] Transform[] sprites;

    private float viewHeight;
    private float spriteHeight;
    private int startIndex = 0;
    private int endIndex;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
        spriteHeight = sprites[0].GetComponent<SpriteRenderer>().bounds.size.y;
        endIndex = sprites.Length - 1;
    }

    private void Update()
    {
        Vector3 moveDirection = Vector3.down * speed * Time.deltaTime;
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].transform.position += moveDirection;
        }

        if (sprites[endIndex].position.y < -viewHeight * 2)
        {
            Vector3 topSpritePos = sprites[startIndex].transform.position;
            sprites[endIndex].transform.position = topSpritePos + Vector3.up * spriteHeight;

            startIndex = endIndex;
            endIndex = (endIndex - 1 + sprites.Length) % sprites.Length;
        }
    }
}
