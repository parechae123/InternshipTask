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
        NodeTransform.GetComponent<SpriteRenderer>().sprite = (Sprite)ResourceManager.GetInstance.preLoaded["block_normal_stage_11"]; 
        NodeBase.Factory(NodeType.obstacled, NodeTransform);
    }
    public ObstacleNode(Transform nodeCenter)
    {
        NodeTransform = nodeCenter;
    }
}
