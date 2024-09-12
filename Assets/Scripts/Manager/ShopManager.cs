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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetRandomItems();
        }
    }

    public void OpenShop()
    {
        // 상점 UI 열기
    }

    public void GetRandomItems()
    {
        for (int i = 0; i < 9; i++)
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

        ItemManager.Instance.shopItemList.Clear();
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
