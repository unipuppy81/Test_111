using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class ItemManager : Singleton<ItemManager>
{
    [Header("�κ��丮")]
    [SerializeField] private GameObject[] slots;

    [SerializeField] private List<Item> myItemList, nonTypeItemList, miniItemList;
    [SerializeField] private List<Item> totalItemListOrgin;
    
    public List<Item> totalItemList;


    [Header("����")]
    public List<Item> shopItemList;

    private void Start()
    {
        SlotSetting();
        ItemListSetting();
    }



    #region �κ��丮

    public int FindItem(string itemType, string itemAttribute)
    {
        int itemId = 99999;

        if (Enum.TryParse(itemType, out ItemType parsedItemType) &&
       Enum.TryParse(itemAttribute, out AttributeType parsedAttributeType))
        {
            Item curItem = totalItemList.Find(x => x.itemType == parsedItemType && x.attribute == parsedAttributeType);
            itemId = curItem.itemId;
        }

        return itemId;
    }

    public void GetItem(int itemId)
    {
        Item curItem = myItemList.Find(x => x.itemId.Equals(itemId));

        if (curItem != null)
        {
            curItem.itemLevel = curItem.itemLevel + 1;
            PlayerStats.Instance.AddItem(curItem.itemType);
            PlayerStats.Instance.AddAttribute(curItem.attribute);

            if(curItem.itemLevel == 7)
            {
                totalItemList.RemoveAll(x => x.itemId == curItem.itemId);
            }
        }
        else
        {
            Item findItem = totalItemList.Find(x => x.itemId.Equals(itemId));
            if (findItem != null)
            {
                Debug.Log(findItem.itemLevel);
                findItem.itemLevel++;
                findItem.CheckItemSet();
                myItemList.Add(findItem);
                PlayerStats.Instance.AddItem(findItem.itemType);
                PlayerStats.Instance.AddAttribute(findItem.attribute);
            }
        }


        nonTypeItemList.Add(myItemList.Find(x => x.attribute.Equals("None")));
        myItemList.RemoveAll(x => x.attribute == AttributeType.None);

        SlotSetting();
                    UiManager.Instance.UpdateSlot();
    }

    public void SlotSetting()
    {
        for(int i =0;i < slots.Length; i++)
        {
            Slots s = slots[i].GetComponent<Slots>();

            bool isExist = i < myItemList.Count;
            slots[i].SetActive(isExist);

            if (isExist)
            {
                s.curItem = myItemList[i];
                s.UpdateSlot();
            }
        }
    }

    public void ItemListSetting()
    {
        foreach (Item item in totalItemListOrgin)
        {
            Item clone = item.Clone();
            totalItemList.Add(clone);
        }
    }
    #endregion

    #region ����
    public void GetShopItem(int itemId)
    {
        Item curItem = totalItemList.Find(x => x.itemId.Equals(itemId));

        if (curItem != null)
        {
            shopItemList.Add(curItem);
        }
    }
    #endregion

    #region ���� & �ε�
    void Save()
    {
        string jdata = JsonConvert.SerializeObject(myItemList);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata);
    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        myItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);
    }
    #endregion
}
