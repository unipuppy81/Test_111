using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    [Header("무기")]
    public int[] itemLevelData;
    public string[] itemSwordLevelDataDesc;
    public string[] itemShieldLevelDataDesc;
    public string[] itemArrowLevelDataDesc;

    [Header("속성")]
    public int[] attributeLevelData;
    public string[] attributeFireLevelDataDesc;
    public string[] attributeWindLevelDataDesc;
}
