using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI count;



    public void SetSlot(Sprite s, int c)
    {
        icon.sprite = s;
        count.text = c.ToString();
    }
}