using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCtrler : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else
            {
                UIManager.GetInstance.TradeButtonReset();
            }
        }
    }
}
