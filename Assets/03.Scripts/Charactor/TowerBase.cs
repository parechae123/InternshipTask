using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase
{
    public string GetCodeName { get { return grade+towerName; } }
    public string towerName;
    public CharacterGrade grade;
    public AttackModuleType moduleType;
    public NodeBase standingNode;
    private TowerEntity tower;
    public TowerBase(NodeBase standing,ParsingData.UnitData data)
    {
        standingNode = standing;
        towerName = data.dataname;
        grade = data.grade;
        moduleType = data.attackmodule;
        tower = GameManager.GetInstance.towerPool.DeQueue();
        tower.Init(data);
        tower.transform.parent = standing.NodeTransform;
        tower.transform.position = standing.NodeTransform.position + Vector3.up * 0.5f;
        GameManager.GetInstance.RegistTower(this);
    }
    public void TowerDelete()
    {
        standingNode.RemoveBuild();
        GameManager.GetInstance.towerPool.EnQueue(tower);
    }
}
public enum CharacterGrade : ushort
{
    common,unCommon,rare,superRare
}