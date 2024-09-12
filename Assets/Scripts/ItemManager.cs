using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class ItemManager : Singleton<ItemManager>
{
    [Header("인벤토리")]
    [SerializeField] private GameObject[] slots;

    [SerializeField] private List<Item> miniItemList;
    [SerializeField] private List<Item> totalItemListOrgin;
    
    public List<Item> totalItemList;
    public List<Item> myItemList, nonTypeItemList;

    [Header("상점")]
    public List<Item> shopItemList;

    private void Start()
    {
        SlotSetting();
        ItemListSetting();
    }



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
            PlayerData.Instance.AddItem(curItem.itemType);
            PlayerData.Instance.AddAttribute(curItem.attribute);
            PlayerData.Instance.SetItemValue(curItem);

            if (curItem.itemLevel == 7)
            {
                totalItemList.RemoveAll(x => x.itemId == curItem.itemId);
            }
        }
        else
        {
            Item findItem = totalItemList.Find(x => x.itemId.Equals(itemId));
            if (findItem != null)
            {
                findItem.itemLevel++;
                findItem.CheckItemSet();
                myItemList.Add(findItem);
                PlayerData.Instance.AddItem(findItem.itemType);
                PlayerData.Instance.AddAttribute(findItem.attribute);
                PlayerData.Instance.SetItemValue(findItem);
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

    #region 상점
    public void GetShopItem(int itemId)
    {
        Item curItem = totalItemList.Find(x => x.itemId.Equals(itemId));

        if (curItem != null)
        {

            if (curItem.attribute != AttributeType.None)
            {
                curItem.itemTypeCount = PlayerData.Instance.GetItemTypeCount(curItem.itemType);
                curItem.attributeTypeCount = PlayerData.Instance.GetAttributeTypeCount(curItem.attribute);
            }

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
