using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TradeBTN : MonoBehaviour
{
    [SerializeField]private Button button;
    [SerializeField]private TextMeshProUGUI value;
    GameManager gm { get { return GameManager.GetInstance; } }
    public void OnSummonClick()
    {
        GameManager.GetInstance.currGold -= 20;
        gm.SummonTower(gm.selectedNode, gm.GetSummonGrade());
        UIManager.GetInstance.TradeButtonReset();
    }
    public void OnShiftClick()
    {
        GameManager.GetInstance.currMineral -= (ushort)(((ushort)gm.selectedNode.Builded.grade + 1) * 10);

        gm.SummonTower(gm.selectedNode, gm.selectedNode.Builded.grade);
        UIManager.GetInstance.TradeButtonReset();
    }
    public void OnFixClick()
    {
        GameManager.GetInstance.currMineral -= GameManager.GetInstance.fixPrice;
        NodeSetter setter = gm.selectedNode.NodeTransform.GetComponent<NodeSetter>();
        setter.OnNodeChangeToPlaceable();
        UIManager.GetInstance.TradeButtonReset();
        GameManager.GetInstance.fixPrice += 10;
    }
    public void OnUpgradeClick()
    {
        gm.UpGradeTower(gm.selectedNode);
        UIManager.GetInstance.TradeButtonReset();
    }
    public void SetEnable(Vector3 worldPosition,bool condition,int price)
    {
        gameObject.SetActive(true);
        transform.position = Camera.main.WorldToScreenPoint(worldPosition);
        button.interactable = condition;
         
        if(value != null)value.text = price.ToString();
    }
}
