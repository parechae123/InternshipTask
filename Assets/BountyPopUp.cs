using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BountyPopUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject bountyPannel;
    void Awake()
    {
        UIManager.GetInstance.bountyPannel = bountyPannel;
    }
    public void OnButtonClick()
    {
        UIManager.GetInstance.bountyPannel.SetActive(!UIManager.GetInstance.bountyPannel.activeSelf);
    }
}
