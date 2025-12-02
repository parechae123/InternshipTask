using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPannel : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
    public void OnReTry()
    {
        GameManager.GetInstance.Reset();
        SceneManager.LoadScene(0);
    }
    public void OnExit()
    {
        Application.Quit();
    }
}
