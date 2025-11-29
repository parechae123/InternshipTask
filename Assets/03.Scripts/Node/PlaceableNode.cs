using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableNode : NodeBase
{
    public override Transform NodeTransform { get; set; }
    public TowerBase builded;
    public override TowerBase Builded { get { return builded; } }

    public override NodeType GetNodeType { get { return NodeType.placeAble; } }

    public override void OnClick()
    {
        //소환 ui버튼 팝업

    }
    public PlaceableNode(Transform nodeCenter)
    {
        NodeTransform = nodeCenter;
    }
}
