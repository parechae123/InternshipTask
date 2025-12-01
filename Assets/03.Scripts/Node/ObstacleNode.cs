using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleNode : NodeBase
{
    public override Transform NodeTransform { get; set; }
    public override TowerBase Builded { get { return null; } }

    public override NodeType GetNodeType { get { return NodeType.obstacled; } }

    public override void OnClick()
    {
        //UI 출력 후 이걸 해줘야함
        UIManager.GetInstance.TradeButtonReset();
        UIManager.GetInstance.fixBTN.SetEnable(NodeTransform.position + Vector3.up
            , GameManager.GetInstance.fixPrice <= GameManager.GetInstance.currMineral
            , GameManager.GetInstance.fixPrice);
    }
    public ObstacleNode(Transform nodeCenter)
    {
        NodeTransform = nodeCenter;
    }
}
