using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using JetBrains.Annotations;

public class ItemManager : Singleton<ItemManager>
{
    [Header("인벤토리")]
    [SerializeField] private GameObject[] slots;

    [SerializeField] private List<Item> myItemList, curItemList;
    [SerializeField] private List<Item> totalItemList;

    [Header("상점")]
    public List<Item> shopItemList;

    #region 인벤토리

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
        }
        else
        {
            Item findItem = totalItemList.Find(x => x.itemId.Equals(itemId));
            if (findItem != null)
            {
                findItem.itemLevel = 1;
                findItem.CheckItemSet();
                myItemList.Add(findItem);
            }
        }

       
        

        /*
        myItemList.Sort((p1, p2) =>
        {
            try
            {
                int index1 = int.Parse(p1.s_ItemID);
                int index2 = int.Parse(p2.s_ItemID);
                return index1.CompareTo(index2);
            }
            catch (FormatException)
            {
                return p1.s_ItemID.CompareTo(p2.s_ItemID);
            }
        });
        */
        


        Save();
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
    #endregion

    #region 상점
    public void GetShopItem(int itemId)
    {
        Item curItem = totalItemList.Find(x => x.itemId.Equals(itemId));

        if (curItem != null)
        {
            shopItemList.Add(curItem);
        }
    }
    #endregion

    #region 저장 & 로드
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
