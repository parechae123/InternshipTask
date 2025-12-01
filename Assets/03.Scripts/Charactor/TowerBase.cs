using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase
{
    public string GetCodeName { get { return grade+towerName; } }
    public string towerName;
    public CharacterGrade grade;
    public NodeBase standingNode;
    public GameObject tower;
    public TowerBase(NodeBase standing,ParsingData.UnitData data)
    {
        standingNode = standing;

    }
    public void TowerDelete()
    {
        GameManager.GetInstance.RemoveTower(this);
        standingNode.RemoveBuild();
    }
}
public enum CharacterGrade : ushort
{
    common,unCommon,rare,superRare
}