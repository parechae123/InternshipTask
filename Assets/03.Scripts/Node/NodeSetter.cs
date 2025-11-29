using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeSetter : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] public NodeType nodeType;
    private NodeBase node;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.GetInstance.selectedNode = node;
        node.OnClick();
    }

    void Start()
    {
        node = NodeBase.Factory(nodeType, transform);
    }

}
