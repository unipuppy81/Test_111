using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;

public class ShopSlot : MonoBehaviour
{
    [Header("Title")]
    [SerializeField] private Image typeImage;
    [SerializeField] private Image typeBg;
    [SerializeField] private Image totalBg;

    [Header("Cost")]
    [SerializeField] private TextMeshProUGUI costText;

    [Header("Item Info")]
    [SerializeField] private Image itemNameBg;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;

    [Header("Item Count")]
    [SerializeField] private GameObject bottonDesc;
    [SerializeField] private Image icon_Type;
    [SerializeField] private Image icon_Attribute;
    [SerializeField] private TextMeshProUGUI count_Type;
    [SerializeField] private TextMeshProUGUI count_Attribute;

    [Header("Item")]
    public Item shopItem;
    public string itemType;
    public string itemAttribute;
    public int itemId;

    [Header("Var")]
    [SerializeField] private Color color;


    public void SetShopSlot()
    {
        bottonDesc.SetActive(true);

        typeImage.sprite = shopItem.itemIcon;
        itemType = shopItem.itemType.ToString();
        itemAttribute = shopItem.attribute.ToString();
        costText.text = shopItem.itemCost.ToString();
        itemName.text = shopItem.itemName;
        itemDesc.text = shopItem.itemDesc;

        //itemCount
        // 게임메니저에서 따로 관리

        SetColor();
        SetAttribute();
    }

    private void SetColor()
    {
        GameManager.Instance.colorDictionary.TryGetValue(itemAttribute, out color);
        if(color != null)
        {
            typeBg.color = new Color(color.r, color.g, color.b, 0.6f);
            totalBg.color = new Color(color.r, color.g, color.b, 0.3f);
            itemNameBg.color = new Color(color.r, color.g, color.b, 0.8f);
            icon_Attribute.color = color;
            count_Attribute.color = color;
        }
    }
    private void SetAttribute()
    {
        if (itemAttribute.Equals("None"))
            bottonDesc.SetActive(false);
    }
}
