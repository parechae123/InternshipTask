using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
public class NodeSetter : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] public NodeType nodeType;
    private NodeBase node;
    private void Awake()
    {
        Debug.Log(UIManager.GetInstance);
        Debug.Log(GameManager.GetInstance);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.GetInstance.selectedNode = node;
        node.OnClick();
    }

    void Start()
    {
        node = NodeBase.Factory(nodeType, transform);
    }
    public void OnNodeChangeToPlaceable()
    {
        node = NodeBase.Factory(NodeType.placeAble, transform);
        //TODO : 스테이지 추가 시 해당 스프라이트 키값 변경 요망
        if (ResourceManager.GetInstance.preLoaded.TryGetValue("TileMapSprites", out object result))
        {
            transform.GetComponent<SpriteRenderer>().sprite = ((SpriteAtlas)result).GetSprite("block_normal_stage_11");
        }
        else
        {
            Debug.LogError("block_normal_stage_11 Sprite를 불러오는데에 실패했습니다.");
        }
    }
}
