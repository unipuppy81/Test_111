using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    [Header("플레이어 스탯")]
    public int gold = 0;
    public Dictionary<ItemType, int> itemTypeCount = new Dictionary<ItemType, int>();
    public Dictionary<AttributeType, int> attributeTypeCount = new Dictionary<AttributeType, int>();


    private void Awake()
    {
        SetStats();
        gold = 0;
    }

    #region 플레이어 스탯

    private void SetStats()
    {
        itemTypeCount.Add(ItemType.Sword, 0);
        itemTypeCount.Add(ItemType.Shield, 0);
        itemTypeCount.Add(ItemType.Arrow, 0);

        attributeTypeCount.Add(AttributeType.Fire, 0);
        attributeTypeCount.Add(AttributeType.Wind, 0);
    }

    public void AddItem(ItemType itemType)
    {
        if (itemTypeCount.ContainsKey(itemType))
        {
            itemTypeCount[itemType]++;
        }
    }

    public void AddAttribute(AttributeType attributeType)
    {
        if (attributeTypeCount.ContainsKey(attributeType))
        {
            attributeTypeCount[attributeType]++;
        }
    }

    public int GetItemTypeCount(ItemType itemType)
    {
        if (itemTypeCount.ContainsKey(itemType))
        {
            return itemTypeCount[itemType];
        }
        return 0;
    }

    public int GetAttributeTypeCount(AttributeType attributeType)
    {
        if (attributeTypeCount.ContainsKey(attributeType))
        {
            return attributeTypeCount[attributeType];
        }
        return 0;
    }


    public string GetItemProgress(string itemTypeString)
    {
        if (!Enum.TryParse(itemTypeString, out ItemType itemType))
        {
            return "Invalid item type";
        }

        int currentCount = itemTypeCount.ContainsKey(itemType) ? itemTypeCount[itemType] : 0;
        int nextGoal = -1;

        foreach (int level in GameManager.Instance.itemData.itemLevelData)
        {
            if (level > currentCount)
            {
                nextGoal = level;
                break; 
            }
        }

        if (nextGoal == -1)
        {
            nextGoal = 7;
            currentCount = 7;
        }

        return $"{currentCount} / {nextGoal}";
    }

    public string GetAttributeProgress(string AttributeTypeString)
    {
        if (!Enum.TryParse(AttributeTypeString, out AttributeType attributeType))
        {
            return "Invalid item type";
        }

        int currentCount = attributeTypeCount.ContainsKey(attributeType) ? attributeTypeCount[attributeType] : 0;
        int nextGoal = -1;

        foreach (int level in GameManager.Instance.itemData.attributeLevelData)
        {
            if (level > currentCount)
            {
                nextGoal = level;
                break;
            }
        }

        if (nextGoal == -1)
        {
            nextGoal = 7;
            currentCount = 7;
        }

        return $"{currentCount} / {nextGoal}";
    }
    #endregion
}
