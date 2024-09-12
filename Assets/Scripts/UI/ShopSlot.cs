using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    [Header("Title")]
    [SerializeField] private Image typeImage;
    [SerializeField] private Image typeBg;
    [SerializeField] private Image totalBg;

    [Header("Cost & Special")]
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private GameObject specialObj;
    [SerializeField] private GameObject top;
    [SerializeField] private TextMeshProUGUI specialText;

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
    [SerializeField] private Button thisBtn;

    private void Update()
    {
        float newY = Mathf.Sin(Time.time * 1) * .01f;
        top.transform.position = new Vector3(top.transform.position.x, top.transform.position.y + newY, top.transform.position.z);
    }

    public void SetShopSlot()
    {
        bottonDesc.SetActive(true);
        thisBtn.interactable = true;

        typeImage.sprite = shopItem.itemIcon;
        itemType = shopItem.itemType.ToString();
        itemAttribute = shopItem.attribute.ToString();
        costText.text = shopItem.itemCost.ToString();
        itemName.text = shopItem.itemName;
        itemDesc.text = shopItem.itemDesc;

        count_Type.text = $"{PlayerStats.Instance.GetItemTypeCount(shopItem.itemType)} / {3}";
        count_Attribute.text = $"{PlayerStats.Instance.GetAttributeTypeCount(shopItem.attribute)} / {3}";

        SetColor();
        SetAttribute();
        SpecialPanel();
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

    public void PurchaseItem()
    {
        ItemManager.Instance.GetItem(shopItem.itemId);
        thisBtn.interactable = false;

        if (PlayerStats.Instance.gold >= shopItem.itemCost)
        {
            PlayerStats.Instance.gold -= shopItem.itemCost;
        }
    }

    private void SpecialPanel()
    {
        if(shopItem.itemLevel == 2 || shopItem.itemLevel == 4 || shopItem.itemLevel == 6)
        {
            specialObj.SetActive(true);
        }
        else
        {
            specialObj.SetActive(false);
        }
    }
}
