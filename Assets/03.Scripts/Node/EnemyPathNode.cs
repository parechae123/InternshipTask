using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathNode : NodeBase
{
    public override Transform NodeTransform { get; set; }

    public override TowerBase Builded { get; }

    public override NodeType GetNodeType { get { return NodeType.enemyPath; } }

    public override void OnClick()
    {
        
    }
    public EnemyPathNode(Transform center)
    {
        NodeTransform = center;
    }
}
