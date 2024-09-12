using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExplainSlot : MonoBehaviour
{
    [SerializeField] private Image explainImage;
    [SerializeField] private TextMeshProUGUI explainString;
    [SerializeField] private TextMeshProUGUI explainCountString;

    [SerializeField] private Image first;
    [SerializeField] private Image second;
    [SerializeField] private Image third;

    [SerializeField] private TextMeshProUGUI firstText;
    [SerializeField] private TextMeshProUGUI secondText;
    [SerializeField] private TextMeshProUGUI thirdText;

    [SerializeField] private Button explainBtn_01;
    [SerializeField] private Button explainBtn_02;
    [SerializeField] private Button explainBtn_03;

    [SerializeField] private TextMeshProUGUI[] levelCount;
    [SerializeField] private TextMeshProUGUI[] levelDesc;


    private int curCount = 0;
    private Color firstC;
    private Color secondC;
    private Color thirdC;

    private void Start()
    {
        firstC = Color.gray;
        secondC = Color.gray;
        thirdC = Color.gray;

        first.color = firstC;
        second.color = secondC;
        third.color = thirdC;
    }
    public void CheckCurCount()
    {
        Debug.Log(curCount);
        if (curCount >= 3 && curCount < 5)
        {
            first.color = Color.yellow;
            firstText.color = Color.black;
            explainBtn_01.interactable = true;

            second.color = secondC;
            secondText.color = Color.white;
            explainBtn_02.interactable = false;

            third.color = thirdC;
            thirdText.color = Color.white;
            explainBtn_03.interactable = false;
        }
        else if (curCount >= 5 && curCount < 7)
        {
            second.color = Color.yellow;
            secondText.color = Color.black;
            explainBtn_02.interactable = true;

            first.color = firstC;
            firstText.color = Color.white;
            explainBtn_01.interactable = false;

            third.color = thirdC;
            thirdText.color = Color.white;
            explainBtn_03.interactable = false;
        }
        else if (curCount >= 7)
        {
            third.color = Color.yellow;
            thirdText.color = Color.black;
            explainBtn_03.interactable = true;

            first.color = firstC;
            firstText.color = Color.white;
            explainBtn_01.interactable = false;

            second.color = secondC;
            secondText.color = Color.white;
            explainBtn_02.interactable = false;
        }
        else
        {
            first.color = firstC;
            firstText.color = Color.white;
            explainBtn_01.interactable = false;

            second.color = secondC;
            secondText.color = Color.white;
            explainBtn_02.interactable = false;

            third.color = thirdC;
            thirdText.color = Color.white;
            explainBtn_03.interactable = false;
        }
    }
    public void ExplainSlotSet(Sprite image, string desc, string count, string type)
    {
        explainImage.sprite = image;
        explainString.text = desc;
        explainCountString.text = count;

        switch (type)
        {
            case "Sword":
                curCount = PlayerData.Instance.itemTypeCount[ItemType.Sword];
                for (int i =0;i < levelCount.Length; i++)
                {
                    levelCount[i].text = GameManager.Instance.itemData.itemLevelData[i].ToString();
                    levelDesc[i].text = GameManager.Instance.itemData.itemSwordLevelDataDesc[i];
                }
                break;
            case "Shield":
                curCount = PlayerData.Instance.itemTypeCount[ItemType.Shield];
                for (int i = 0; i < levelCount.Length; i++)
                {
                    levelCount[i].text = GameManager.Instance.itemData.itemLevelData[i].ToString();
                    levelDesc[i].text = GameManager.Instance.itemData.itemShieldLevelDataDesc[i];
                }
                break;
            case "Arrow":
                curCount = PlayerData.Instance.itemTypeCount[ItemType.Arrow];
                for (int i = 0; i < levelCount.Length; i++)
                {
                    levelCount[i].text = GameManager.Instance.itemData.itemLevelData[i].ToString();
                    levelDesc[i].text = GameManager.Instance.itemData.itemArrowLevelDataDesc[i];
                }
                break;
            case "Fire":
                Color c = Color.red;
                explainImage.color = new Color(c.r, c.g, c.b, 0.75f);
                curCount = PlayerData.Instance.attributeTypeCount[AttributeType.Fire];
                for (int i = 0; i < levelCount.Length; i++)
                {
                    levelCount[i].text = GameManager.Instance.itemData.attributeLevelData[i].ToString();
                    levelDesc[i].text = GameManager.Instance.itemData.attributeFireLevelDataDesc[i];
                }
                break;
            case "Wind":
                Color g = Color.green;
                explainImage.color = new Color(g.r, g.g, g.b, 0.75f);
                curCount = PlayerData.Instance.attributeTypeCount[AttributeType.Wind];
                for (int i = 0; i < levelCount.Length; i++)
                {
                    levelCount[i].text = GameManager.Instance.itemData.attributeLevelData[i].ToString();
                    levelDesc[i].text = GameManager.Instance.itemData.attributeWindLevelDataDesc[i];
                }
                break;
        }

        CheckCurCount();
    }
}
