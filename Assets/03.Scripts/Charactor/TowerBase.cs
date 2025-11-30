using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase
{
    public string GetCodeName { get { return grade+towerName; } }
    public string towerName;
    public CharactorGrade grade;
    public NodeBase standingNode;

    /// <summary>
    /// Queue 풀링필요
    /// </summary>
    public void TowerDelete()
    {

    }
}
public enum CharactorGrade
{
    common,unCommon,rare,superRare
}