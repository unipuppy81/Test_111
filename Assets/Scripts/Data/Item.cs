using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Sword, Shield, Arrow, Attack, Gold, Heal }
public enum AttributeType { None, Fire, Wind }


[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/Item", order = 1)]
public class Item : ScriptableObject
{
    [Header("Info")]
    public ItemType itemType;
    public AttributeType attribute;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public int itemLevel;
    public int itemCost;
    public Sprite itemIcon;

    [Header("Shop && NonSkill")]
    public int itemTypeCount;
    public int attributeTypeCount;
    public float damageIncrease;
    public float speedIncrease;
    public int goldAmount;
    public float healAmount;
    public void CheckItemSet()
    {
        LevelUp();
    }

    public void LevelUp()
    {
        if (itemLevel < 7)
        {
            ApplyLevelBonuses();
        }
    }

    private void ApplyLevelBonuses()
    {
        // 3, 5, 7������ �� ���ʽ� ����
        if (itemLevel == 3 || itemLevel == 5 || itemLevel == 7)
        {
            // �����ۿ� ���� ���ʽ� ����
            if (itemType == ItemType.Sword)
            {
                // Sword ������ ���ʽ� ����
            }
            else if (itemType == ItemType.Shield)
            {
                // Shield ���ʽ� ����
            }
            else if (itemType == ItemType.Arrow)
            {
                // Arrow ���ʽ� ����
            }
        }
    }


    public Item Clone()
    {
        Item clone = CreateInstance<Item>();
        clone.itemType = this.itemType;
        clone.attribute = this.attribute;
        clone.itemId = this.itemId;
        clone.itemName = this.itemName;
        clone.itemDesc = this.itemDesc;
        clone.itemLevel = this.itemLevel;
        clone.itemCost = this.itemCost;
        clone.itemIcon = this.itemIcon;
        return clone;
    }
}
