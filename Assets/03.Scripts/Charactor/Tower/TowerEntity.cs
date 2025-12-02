using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TowerEntity : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private AttackModule attackModule;
    public void Init(ParsingData.UnitData data)
    {
        if (sr == null) sr.GetComponent<SpriteRenderer>();
        sr.sprite = ((SpriteAtlas)(ResourceManager.GetInstance.preLoaded["UnitSprites"])).GetSprite(data.spriteName);
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
                sr.color = new Color32(255, 194, 205,255);
                break;
        }
        attackModule = AttackModule.Factory(data,transform);
    }
    private void Update()
    {
        attackModule.ReadyAttack();
    }
}

