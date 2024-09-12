using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Slots : MonoBehaviour
{
    [SerializeField] private Image itemIamge;
    [SerializeField] private Image bgImage;
    [SerializeField] private GameObject countTextObj;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Button thisBtn;

    [Header("Item Information")]
    public Item curItem;

    public void UpdateSlot()
    {
        itemIamge.sprite = curItem.itemIcon;
        
        if(curItem.itemLevel > 1)
        {
            countTextObj.SetActive(true);
            countText.text = "x" + curItem.itemLevel.ToString();
        }

        if (curItem.attribute == AttributeType.Fire)
        {
            bgImage.color = Color.red;
        }
        else if (curItem.attribute == AttributeType.Wind)
        {
            bgImage.color = Color.green;
        }
    }

    public void OnClickSlot()
    {
        UiManager.Instance.UpdateItemExplain(curItem.itemIcon, curItem.itemName, curItem.itemDesc);
    }
}
