using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Sword, Shield, Arrow, Attack, Gold, Heal }
public enum AttributeType { None, Fire, Wind }


[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData", order = 1)]
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

    [Header("Level Data")]
    public float baseDamage;
    public int baseCount;
    public float damageIncrease;

    public void CheckItemSet()
    {
        LevelUp();
    }

    public void LevelUp()
    {
        if (itemLevel < 7)
        {
            itemLevel++;
            ApplyLevelBonuses();
        }
    }

    private void ApplyLevelBonuses()
    {
        // 3, 5, 7레벨일 때 보너스 적용
        if (itemLevel == 3 || itemLevel == 5 || itemLevel == 7)
        {
            // 아이템에 따른 보너스 로직
            if (itemType == ItemType.Sword)
            {
                // Sword 레벨별 보너스 적용
            }
            else if (itemType == ItemType.Shield)
            {
                // Shield 보너스 적용
            }
            else if (itemType == ItemType.Arrow)
            {
                // Arrow 보너스 적용
            }
        }
    }
}
