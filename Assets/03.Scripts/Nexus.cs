using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Nexus : MonoBehaviour,IPointerClickHandler
{
    bool onRight;
    byte workerNum = 1;
    byte workerMax = 20;
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.GetInstance.workerProducer.pannel.SetActive(true);
    }

    public void OnProduceBTNClick()
    {
        workerNum++;
        bool isMax = workerNum >= workerMax;
        GameManager.GetInstance.isMaxWorker = isMax;
        if(isMax) UIManager.GetInstance.workerProducer.produceButton.interactable = !isMax;
        UIManager.GetInstance.workerText.text = $"{workerNum}/{workerMax}";
        ushort price = GameManager.GetInstance.WorkerPrice;
        GameManager.GetInstance.WorkerPrice += 5;
        GameManager.GetInstance.CurrGold -= price;
        onRight = !onRight;
        Vector3 currPos = transform.position + Vector3.right * (onRight ? 3 : -3);
        CreateWorker().Init(onRight, currPos);
    }

    public Worker CreateWorker()
    {
        Worker worker = GameObject.Instantiate((GameObject)ResourceManager.GetInstance.preLoaded["Worker"]).GetComponent<Worker>();
        return worker;
    }
}
public class NexusManaging
{
    
}