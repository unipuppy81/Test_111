using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum SlotType { Item, Attribute }

public class AttributeSlot : MonoBehaviour
{
    public SlotType slotType;
    public Image icon;
    public string typeKor;
    public string type;
    public TextMeshProUGUI countText;
    public GameObject topObject;
    public TextMeshProUGUI topText;

    private int curLevel = 0;

    public void UpdateThisSlot()
    {
        if(slotType == SlotType.Item)
        {
            countText.text = PlayerStats.Instance.GetItemProgress(type);

            switch (type)
            {
                case "Sword":
                    curLevel = PlayerStats.Instance.GetItemTypeCount(ItemType.Sword);
                    break;

                case "Shield":
                    curLevel = PlayerStats.Instance.GetItemTypeCount(ItemType.Shield);
                    break;

                case "Arrow":
                    curLevel = PlayerStats.Instance.GetItemTypeCount(ItemType.Arrow);
                    break;
            }
        
        }
        else if (slotType == SlotType.Attribute)
        {
            countText.text = PlayerStats.Instance.GetAttributeProgress(type);

            switch (type)
            {
                case "Fire":
                    curLevel = PlayerStats.Instance.GetAttributeTypeCount(AttributeType.Fire);
                    break;

                case "Wind":
                    curLevel = PlayerStats.Instance.GetAttributeTypeCount(AttributeType.Wind);
                    break;
            }
        }


        if(curLevel >= 3 && curLevel < 5)
        {
            topObject.SetActive(true);
            topText.text = "1";
        }
        else if(curLevel >= 5 && curLevel < 7)
        {
            topObject.SetActive(true);
            topText.text = "2";
        }
        else if(curLevel >= 7)
        {
            topObject.SetActive(true);
            topText.text = "3";
        }
    }

    public void UpdateExplainPanel()
    {
        UiManager.Instance.UpdateExplainPanel(icon, typeKor, countText.text, type);
    }
}
