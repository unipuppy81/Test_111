using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : Singleton<UiManager>
{
    [Header("변수")]
    [SerializeField] private ShopManager shopManager;

    [Header("게임")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject flag_Middle;
    [SerializeField] private GameObject flag_End;
    [SerializeField] private TextMeshProUGUI meterText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Button invenBtn;


    [Header("스탯")]
    [SerializeField] private GameObject miniStatsPanel;
    [SerializeField] private GameObject statsPanel;

    [Header("설명")]
    [SerializeField] private GameObject explainWeaponPanel;
    [SerializeField] private GameObject explainAttributePanel;
    [SerializeField] private GameObject explainItemPanel;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI desc;

    [SerializeField] private GameObject[] itemSlot, attributeSlot;

    [Header("상점")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TextMeshProUGUI goldText;

    #region 게임 설정
    private void Start()
    {
        statsPanel.SetActive(false);
        shopPanel.SetActive(false);
        UpdateSlot();
        GoldTextUpdate();
        slider.maxValue = GameManager.Instance.secondBossSpawnTime;
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

        SliderUpdate();
    }
    #endregion

    #region 게임 플레이
    public void HpSliderUpdate()
    {
        StartCoroutine(UpdateHpSlider());
    }

    private IEnumerator UpdateHpSlider()
    {
        float currentHp = hpSlider.value;
        float targetHp = GameManager.Instance.player.curHp / GameManager.Instance.player.maxHp; 

        float duration = 0.5f; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            hpSlider.value = Mathf.Lerp(currentHp, targetHp, elapsedTime / duration); 
            yield return null;
        }

        hpSlider.value = targetHp;
    }

    public void GoldTextUpdate()
    {
        coinText.text = PlayerData.Instance.gold.ToString();
    }

    private void SliderUpdate()
    {
        slider.value = GameManager.Instance.playerMoveTime;

        meterText.text = GameManager.Instance.playerMoveTime.ToString("F1") + "m";
    }

    public void ClickInvenBtn()
    {
        statsPanel.SetActive(true);
        Time.timeScale = 0;
    }
    #endregion

    #region 보유 능력

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
        Time.timeScale = 1.0f;
    }

    #endregion

    #region 상점
    public void OpenShopPanel()
    {
        Time.timeScale = 0.0f;
        shopPanel.SetActive(true);
        PlayerData.Instance.gold += 2;
        ReRoll();
    }

    public void ReRoll()
    {
        if(PlayerData.Instance.gold >= 2)
        {
            shopManager.ReRollBtn();
        }
    }    

    public void ExitBtn()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ItemBtn()
    {
        statsPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void GoldSet()
    {
        goldText.text = PlayerData.Instance.gold.ToString();
    }
    #endregion
}
