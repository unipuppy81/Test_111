using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExplainSlot : MonoBehaviour
{
    [SerializeField] private Image explainImage;
    [SerializeField] private TextMeshProUGUI explainString;
    [SerializeField] private TextMeshProUGUI explainCountString;

    [SerializeField] private Button explainBtn_01;
    [SerializeField] private Button explainBtn_02;
    [SerializeField] private Button explainBtn_03;

    public void ExplainSlotSet(Sprite image, string desc, string count, string type)
    {
        explainImage.sprite = image;
        explainString.text = desc;
        explainCountString.text = count;

        if(type.Equals("Fire"))
        {
            explainImage.color = Color.red;
        }
        else if (type.Equals("Wind"))
        {
            explainImage.color = Color.green;
        }
    }
}
