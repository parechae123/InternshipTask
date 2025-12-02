using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InGameUICacher : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI mineralText;
    [SerializeField] TextMeshProUGUI workerText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject gameOverPannel;

    private void Awake()
    {
        UIManager.GetInstance.roundText = roundText;
        UIManager.GetInstance.goldText = goldText;
        UIManager.GetInstance.mineralText = mineralText;
        UIManager.GetInstance.workerText = workerText;
        UIManager.GetInstance.timerText = timerText;
        UIManager.GetInstance.gameOverPannel = gameOverPannel;
        mineralText.text = GameManager.GetInstance.CurrMineral.ToString();
        goldText.text = GameManager.GetInstance.CurrGold.ToString();

        Destroy(this);
    }
}
