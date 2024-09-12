 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes
{
    public int fireAttribute = 0;
    public int windAttribute = 0;

    public void IncreaseAttribute(AttributeType attributeType)
    {
        if (attributeType == AttributeType.Fire)
        {
            fireAttribute++;
            ApplyFireBonuses();
        }
        else if (attributeType == AttributeType.Wind)
        {
            windAttribute++;
            ApplyWindBonuses();
        }
    }

    private void ApplyFireBonuses()
    {
        if (fireAttribute == 3)
        {
            // 폭발 데미지 적용
        }
        else if (fireAttribute == 5)
        {
            // 폭발 범위 증가 적용
        }
    }

    private void ApplyWindBonuses()
    {
        // Water 속성 관련 보너스 로직
    }
}
