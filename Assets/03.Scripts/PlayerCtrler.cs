using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCtrler : MonoBehaviour
{
    
    void Update()
    {
        if (!ResourceManager.GetInstance.loadDone) return;
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else
            {
                UIManager.GetInstance.TradeButtonReset();
                UIManager.GetInstance.workerProducer.pannel.SetActive(false);
                UIManager.GetInstance.bountyPannel.SetActive(false);
            }
        }
    }
}
