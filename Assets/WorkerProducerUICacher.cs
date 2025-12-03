using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WorkerProducerUICacher : MonoBehaviour
{

    [SerializeField] GameObject workerProducePannel;
    [SerializeField] Button produceButton;
    [SerializeField] TextMeshProUGUI valueText;
    private void Awake()
    {
        UIManager.GetInstance.workerProducer.pannel = workerProducePannel;
        UIManager.GetInstance.workerProducer.produceButton = produceButton;
        UIManager.GetInstance.workerProducer.valueText = valueText;
        gameObject.SetActive(false);
        Destroy(this);
    }
}
public class WorkerProducer
{
    public GameObject pannel;
    public Button produceButton;
    public TextMeshProUGUI valueText;
}