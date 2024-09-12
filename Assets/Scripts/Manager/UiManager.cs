using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : Singleton<UiManager>
{
    [Header("����")]
    [SerializeField] private GameObject miniStatsPanel;
    [SerializeField] private GameObject statsPanel;

    [Header("����")]
    [SerializeField] private GameObject explainWeaponPanel;
    [SerializeField] private GameObject explainAttributePanel;
    [SerializeField] private GameObject explainItemPanel;
    [SerializeField] private GameObject[] itemSlot, attributeSlot;

    [Header("����")]
    [SerializeField] private GameObject shopPanel;

    #region ���� ����
    private void Start()
    {
        UpdateSlot();

        statsPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (explainWeaponPanel.activeSelf || explainAttributePanel.activeSelf)
            {
                explainWeaponPanel.SetActive(false);
                explainAttributePanel.SetActive(false);
            }
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

    public void GameExit()
    {

    }

    public void Continue()
    {
        statsPanel.SetActive(false);
    }

    #endregion

    #region ����
    #endregion
}
