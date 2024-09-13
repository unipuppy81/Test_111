using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }


    private void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(PlayerData.Instance.transform.position);
    }
}
