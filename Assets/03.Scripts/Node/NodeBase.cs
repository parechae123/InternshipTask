using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeBase
{
    public abstract Transform NodeTransform { get; set; }
    public abstract TowerBase Builded { get;}
    public abstract NodeType GetNodeType{get;}
    public abstract void OnClick();

    public static NodeBase Factory(NodeType type,Transform nodeTR)
    {
        switch (type)
        {
            case NodeType.none:
                return null;
            case NodeType.enemyPath:
                return new EnemyPathNode(nodeTR);
            case NodeType.obstacled:
                return new ObstacleNode(nodeTR);
            case NodeType.placeAble:
                return new PlaceableNode(nodeTR);
            default:
                return null;
        }
    }
}
public enum NodeType
{
    none,enemyPath,obstacled,placeAble
}