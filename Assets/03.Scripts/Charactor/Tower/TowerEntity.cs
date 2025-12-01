using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEntity : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private AttackModule attackModule;
    public void Init(ParsingData.UnitData data)
    {
        if (sr == null) sr.GetComponent<SpriteRenderer>();
        sr.sprite = (Sprite)ResourceManager.GetInstance.preLoaded[data.spriteName];
        switch (data.grade)
        {
            case CharacterGrade.common:
                sr.color = Color.white;
                break;
            case CharacterGrade.unCommon:
                sr.color = Color.cyan;
                break;
            case CharacterGrade.rare:
                sr.color = Color.red;
                break;
            case CharacterGrade.superRare:
                sr.color = new Color(255, 194, 205);
                break;
        }
        attackModule = AttackModule.Factory(data);
    }
}

