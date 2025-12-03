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
        if (GameManager.GetInstance.CurrGold < 20) return;
        GameManager.GetInstance.CurrGold -= 20;
        gm.SummonTower(gm.selectedNode, gm.GetSummonGrade());
        UIManager.GetInstance.TradeButtonReset();
    }
    public void OnShiftClick()
    {
        ushort price = (ushort)(((ushort)gm.selectedNode.Builded.grade + 1) * 10);
        if (GameManager.GetInstance.CurrMineral < price) return;

        GameManager.GetInstance.CurrMineral -= price;

        gm.SummonTower(gm.selectedNode, gm.selectedNode.Builded.grade, gm.selectedNode.Builded.moduleType);
        UIManager.GetInstance.TradeButtonReset();
    }
    public void OnFixClick()
    {
        if (GameManager.GetInstance.CurrMineral < GameManager.GetInstance.fixPrice) return;
        GameManager.GetInstance.CurrMineral -= GameManager.GetInstance.fixPrice;
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
    public void SetEnable(Vector3 worldPosition,bool condition,ushort price)
    {
        gameObject.SetActive(true);
        transform.position = Camera.main.WorldToScreenPoint(worldPosition);
        button.interactable = condition;

        if(value != null)value.text = price.ToString();
    }
}
