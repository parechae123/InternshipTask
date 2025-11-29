using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    Dictionary<string, TowerBase> towers;
    public NodeBase selectedNode;
    protected override void Init()
    {
        towers = new Dictionary<string, TowerBase>();
    }
    public override void Reset()
    {
        towers.Clear();
    }
}
