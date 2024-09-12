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


    public void UpdateThisSlot()
    {
        if(slotType == SlotType.Item)
        {
            countText.text = PlayerStats.Instance.GetItemProgress(type);
        }
        else if (slotType == SlotType.Attribute)
        {
            countText.text = PlayerStats.Instance.GetAttributeProgress(type);
        }

        UpdateExplainPanel();
    }


    public void UpdateExplainPanel()
    {
        UiManager.Instance.UpdateExplainPanel(icon, typeKor, countText.text, type);
    }
}
