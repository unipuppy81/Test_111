using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    [Header("플레이어 스탯")]
    public Player player;
    public int gold = 0;
    public Dictionary<ItemType, int> itemTypeCount = new Dictionary<ItemType, int>();
    public Dictionary<AttributeType, int> attributeTypeCount = new Dictionary<AttributeType, int>();


    [Header("스킬")]
    public float swordDamage;
    public float shieldDamage;
    public float arrowDamage;
    public float arrowCoolTime;

    // Shield
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject[] firstShield, secondShield;
    private float rotationSpeed = 100.0f;
    private bool isRotate = false;

    // Arrow
    [SerializeField] private GameObject Hunter;


    [Header("속성")]


    private Hunter h;
    private void Start()
    {
        SetStats();
    }

    private void Update()
    {
        if(isRotate)
        {
            RotateShield();
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log(GetAttributeTypeCount(AttributeType.Fire));
            Debug.Log(GetAttributeTypeCount(AttributeType.Wind));
        }
    }

    #region 플레이어 스탯 + 아이템

    private void SetStats()
    {
        itemTypeCount.Add(ItemType.Sword, 0);
        itemTypeCount.Add(ItemType.Shield, 0);
        itemTypeCount.Add(ItemType.Arrow, 0);

        attributeTypeCount.Add(AttributeType.Fire, 0);
        attributeTypeCount.Add(AttributeType.Wind, 0);


        swordDamage = 10;
        shieldDamage = 3; 
        arrowDamage = 2;
        arrowCoolTime = 1.0f;
        gold = 0;

        player.damage = swordDamage;
    }

    public void AddItem(ItemType itemType)
    {
        if (itemTypeCount.ContainsKey(itemType))
        {
            itemTypeCount[itemType]++;
        }

        SwordSkill();
        ShieldSkill();
        ArrowSkill();
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



    #region 플레이어 스킬

    private void SwordSkill()
    {
        Player player = GetComponent<Player>();
        if (GetItemTypeCount(ItemType.Sword) == 3)
        {
            swordDamage *= 1.5f;
            player.damage = swordDamage;
        }
        else if (GetItemTypeCount(ItemType.Sword) == 5)
        {
            player.swordLevel = 5;
        }
        else if (GetItemTypeCount(ItemType.Sword) == 7)
        {
            player.swordLevel = 7;
        }
    }

    private void RotateShield()
    {
        shield.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    public void ShieldSkill()
    {
        if (GetItemTypeCount(ItemType.Shield) == 1)
        {
            for(int i =0;i < firstShield.Length; i++)
            {
                firstShield[i].SetActive(true);
            }

            isRotate = true;
        }
        else if (GetItemTypeCount(ItemType.Shield) == 3)
        {
            shieldDamage = shieldDamage + shieldDamage * 0.3f;

        }
        else if (GetItemTypeCount(ItemType.Shield) == 5)
        {
            for (int i = 0; i < secondShield.Length; i++)
            {
                secondShield[i].SetActive(true);
            }
        }
        else if (GetItemTypeCount(ItemType.Shield) == 7)
        {
            rotationSpeed = 180.0f;
        }

    }

    public void ArrowSkill()
    {
        if (GetItemTypeCount(ItemType.Arrow) == 1)
        {
            GameObject obj = Instantiate(Hunter, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), transform.rotation);
            h = obj.GetComponent<Hunter>();
            h.hunterDamage = arrowDamage;
            h.player = gameObject;
        }
        else if(GetItemTypeCount(ItemType.Arrow) == 3)
        {
            h.SetHunterLevel(3);
        }
        else if (GetItemTypeCount(ItemType.Arrow) == 5)
        {
            arrowDamage *= 1.5f;
            arrowCoolTime *= 0.7f;
            h.SetHunterLevel(5);
            h.hunterDamage = arrowDamage;
            h.SetFireRate(arrowCoolTime);
        }
        else if (GetItemTypeCount(ItemType.Arrow) == 7)
        {
            h.SetHunterLevel(7);
            h.arrowLife = 5;
        }
    }


    #endregion


    #region 능력치 업그레이드

    public void SetItemValue(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.Sword:
                if (item.attribute == AttributeType.Fire)
                {
                    SetAttack(player.damage * item.damageIncrease);
                }
                else if (item.attribute == AttributeType.Wind)
                {
                    SetAttack(player.damage * item.damageIncrease);
                    player.coolTime = (1 - item.speedIncrease) * player.coolTime;
                }
                break;
            case ItemType.Shield:
                if (item.attribute == AttributeType.Fire)
                {
                    shieldDamage = shieldDamage * (1 + item.damageIncrease);
                }
                else if (item.attribute == AttributeType.Wind)
                {
                   rotationSpeed *= (1 + item.speedIncrease);
                }
                break;
            case ItemType.Arrow:
                if (item.attribute == AttributeType.Fire)
                {
                    arrowDamage *= (1+item.damageIncrease);
                    h.hunterDamage = arrowDamage;
                }
                else if (item.attribute == AttributeType.Wind)
                {
                    arrowCoolTime *= (1-item.speedIncrease);
                    h.SetFireRate(arrowCoolTime);
                }
                break;
            case ItemType.Attack:
                player.damage *= (1+item.damageIncrease);
                shieldDamage *= (1+item.damageIncrease);
                arrowDamage *= (1+item.damageIncrease);
                h.hunterDamage = arrowDamage;

                break;

            case ItemType.Gold:

                if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
                {
                    SetGold(item.goldAmount);
                }
                break;

            case ItemType.Heal:
                player.SetHealth(player.curHp * 0.1f);
                break;
        }

    }

    private void SetAttack(float value)
    {
        player.damage += value;
    }

    private void SetGold(int value)
    {
        gold += value;
    }

    #endregion


    #region 플레이어 속성

    public void AttributeSet(Item item)
    {
        switch (item.attribute)
        {
            case AttributeType.Fire:
                if (GetAttributeTypeCount(AttributeType.Fire) == 3)
                {
                    Debug.Log("AAA");
                    player.enemySetterVar = 3;
                }
                else if (GetAttributeTypeCount(AttributeType.Fire) == 5)
                {
                    Debug.Log("bbb");
                    player.enemySetterVar = 5;
                }
                else if (GetAttributeTypeCount(AttributeType.Fire) == 7)
                {
                    Debug.Log("ccc");
                    player.enemySetterVar = 7;
                }
                break;


            case AttributeType.Wind:
                if (GetAttributeTypeCount(AttributeType.Wind) == 3)
                {
                    player.coolTime *= 0.9f;
                    arrowCoolTime *= 0.9f;
                    rotationSpeed *= 0.9f;
                }
                else if (GetAttributeTypeCount(AttributeType.Wind) == 5)
                {
                    player.coolTime *= 0.8f;
                    arrowCoolTime *= 0.8f;
                    rotationSpeed *= 0.8f;
                }
                else if (GetAttributeTypeCount(AttributeType.Wind) == 7)
                {
                    player.coolTime *= 0.5f;
                    arrowCoolTime *= 0.5f;
                    rotationSpeed *= 0.5f;
                }
                break;
        }
        
    }
    #endregion
}
