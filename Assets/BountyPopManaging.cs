using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BountyPopManaging : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]GameObject bountyPannel;
    [SerializeField] Button[] selectButtons;
    [SerializeField] TMPro.TextMeshProUGUI[] timer;
    [SerializeField] BountyData[] data;
    [SerializeField] public ResourceManaging.Pool<BountyEnemyEntity> pool;

    float bountyTime;
    bool isReseted = true;
    void Awake()
    {
        UIManager.GetInstance.bountyPannel = bountyPannel;
        pool = new ResourceManaging.Pool<BountyEnemyEntity>("BountyEnemyEntity");
    }
    public void OnPannelButtonClick()
    {
        UIManager.GetInstance.bountyPannel.SetActive(!UIManager.GetInstance.bountyPannel.activeSelf);
    }
    private void Update()
    {
        if (isReseted) return;
        bountyTime -= Time.deltaTime;
        if (bountyTime <= 0)
        {
            isReseted = true;
            for (int i = 0; i < timer.Length; i++)
            {
                timer[i].text = string.Empty;
                selectButtons[i].interactable = true;
            }
        }
        else
        {
            for (int i = 0; i < timer.Length; i++)
            {
                timer[i].text = bountyTime.ToString("00");
            }
        }
    }
    public void OnFirstBTNClick()
    {
        OnSelectBTNClick(0);
    }
    public void OnSecondBTNClick()
    {
        OnSelectBTNClick(1);
    }
    private void OnSelectBTNClick(int index)
    {
        bountyTime = 30f;
        for (int i = 0; i < selectButtons.Length; i++)
        {
            selectButtons[i].interactable = false;
            timer[i].text = bountyTime.ToString("00");
        }
        isReseted = false;
        GameManager.GetInstance.spawner.SpawnBountyEnemy(pool, data[index], this);
    }

}
