using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slots : MonoBehaviour
{
    [SerializeField] private Image itemIamge;
    [SerializeField] private Image bgImage;
    [SerializeField] private GameObject countTextObj;
    [SerializeField] private TextMeshProUGUI countText;

    [Header("Item Information")]
    public Item curItem;

    public void UpdateSlot()
    {
        Debug.Log("UpdateSlot");
    }

    public void OnClickSlot()
    {

    }
}
