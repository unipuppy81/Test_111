using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject[] shopSlot;

    [SerializeField] private List<int> randomIndexes = new List<int>();
    [SerializeField] private List<int> selectedNumbers = new List<int>();

    public int gold;

    public void ReRollBtn()
    {
        ItemManager.Instance.shopItemList.Clear();
        GetRandomItems();
        PlayerData.Instance.gold -= 2;
        UiManager.Instance.GoldSet();
    }

    public void GetRandomItems()
    {
        for (int i = 0; i < ItemManager.Instance.totalItemList.Count; i++)
        {
            randomIndexes.Add(i);
        }

        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, randomIndexes.Count);
            int selectedNumber = randomIndexes[randomIndex];

            selectedNumbers.Add(selectedNumber);
            randomIndexes.RemoveAt(randomIndex);
        }

        foreach(int index in selectedNumbers)
        {
            ItemManager.Instance.GetShopItem(index);
        }

        randomIndexes.Clear();
        selectedNumbers.Clear();

        SetShopSlot();
    }

    public void SetShopSlot()
    {
        for(int i=0; i < shopSlot.Length; i++)
        {
            ShopSlot s = shopSlot[i].GetComponent<ShopSlot>();
            
            s.shopItem = ItemManager.Instance.shopItemList[i];
            s.SetShopSlot();
        }
    }
}
