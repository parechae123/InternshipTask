using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableNode : NodeBase
{
    public override Transform NodeTransform { get; set; }
    private TowerBase builded;
    public override TowerBase Builded { get { return builded; } }

    public override NodeType GetNodeType { get { return NodeType.placeAble; } }

    public override void OnClick()
    {
        //소환 ui버튼 팝업
        UIManager.GetInstance.TradeButtonReset();
        GameManager.GetInstance.selectedNode = this;
        if (builded == null)//
        {
            UIManager.GetInstance.TradeButtonReset();
            UIManager.GetInstance.summonBTN.SetEnable(NodeTransform.position + Vector3.up
                , 20 <= GameManager.GetInstance.CurrGold
                , 20);
        }
        else
        {
            if (builded.grade >= CharacterGrade.superRare)
            {
                UIManager.GetInstance.TradeButtonReset();
                UIManager.GetInstance.shiftBTN.SetEnable(NodeTransform.position + Vector3.up
                    , 20 <= GameManager.GetInstance.CurrGold
                    , 20);
            }
            else
            {
                UIManager.GetInstance.TradeButtonReset();
                UIManager.GetInstance.shiftBTN.SetEnable(NodeTransform.position + Vector3.up
                    , 20 <= GameManager.GetInstance.CurrGold
                    , 20);
                UIManager.GetInstance.upgradeBTN.SetEnable(NodeTransform.position + Vector3.down
                    , GameManager.GetInstance.SearchDuplicateTower(builded) != null
                    , 0);
            }
        }
    }
    public override void SummonTowerOnTile(ParsingData.UnitData tower)
    {
        if (builded != null)
        {
            GameManager.GetInstance.RemoveTower(builded);
        }
        builded = new TowerBase(this,tower);
    }
    public override void RemoveBuild()
    {
        builded = null;
    }
    public PlaceableNode(Transform nodeCenter)
    {
        NodeTransform = nodeCenter;
    }
}
