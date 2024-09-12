using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : Singleton<UiManager>
{
    [Header("����")]
    [SerializeField] private ShopManager shopManager;

    [Header("����")]
    [SerializeField] private GameObject miniStatsPanel;
    [SerializeField] private GameObject statsPanel;

    [Header("����")]
    [SerializeField] private GameObject explainWeaponPanel;
    [SerializeField] private GameObject explainAttributePanel;
    [SerializeField] private GameObject explainItemPanel;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI desc;

    [SerializeField] private GameObject[] itemSlot, attributeSlot;

    [Header("����")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TextMeshProUGUI goldText;

    #region ���� ����
    private void Start()
    {
        statsPanel.SetActive(false);
        shopPanel.SetActive(false);
        UpdateSlot();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (explainWeaponPanel.activeSelf || explainAttributePanel.activeSelf || explainItemPanel.activeSelf)
            {
                explainWeaponPanel.SetActive(false);
                explainAttributePanel.SetActive(false);
                explainItemPanel.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            UpdateSlot();
        }
    }
    #endregion

    #region �̴� ����
    #endregion

    #region ���� �ɷ�

    public void UpdateSlot()
    {
        for(int i =0;i < itemSlot.Length;i++)
        {
            AttributeSlot aSlot = itemSlot[i].GetComponent<AttributeSlot>();
            aSlot.UpdateThisSlot();
        }

        for(int i =0;i < attributeSlot.Length; i++)
        {
            AttributeSlot aSlot = attributeSlot[i].GetComponent<AttributeSlot>();
            aSlot.UpdateThisSlot();
        }
    }

    public void UpdateExplainPanel(Image image, string desc, string count , string type)
    {
        if(type.Equals("Sword") || type.Equals("Shield") || type.Equals("Arrow"))
        {
            explainWeaponPanel.SetActive(true);
            ExplainSlot exSlot = explainWeaponPanel.GetComponent<ExplainSlot>();
            exSlot.ExplainSlotSet(image.sprite, desc, count, type);
        }
        else if(type.Equals("Fire") || type.Equals("Wind"))
        {
            explainAttributePanel.SetActive(true);
            ExplainSlot exSlot = explainAttributePanel.GetComponent<ExplainSlot>();
            exSlot.ExplainSlotSet(image.sprite, desc, count, type);
        }
    }

    public void UpdateItemExplain(Sprite image, string name, string desc)
    {
        explainItemPanel.SetActive(true);
        icon.sprite = image;
        title.text = name;
        this.desc.text = desc;
    }

    public void GameExit()
    {

    }

    public void Continue()
    {
        statsPanel.SetActive(false);
    }

    #endregion

    #region ����
    public void ReRoll()
    {
        shopManager.ReRollBtn();
    }    

    public void ExitBtn()
    {
        shopPanel.SetActive(false);
    }

    public void ItemBtn()
    {
        statsPanel.SetActive(true);
    }

    public void GoldSet()
    {
        goldText.text = PlayerData.Instance.gold.ToString();
    }
    #endregion
}
