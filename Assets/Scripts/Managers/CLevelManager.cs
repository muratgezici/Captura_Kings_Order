using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class CLevelManager : MonoBehaviour
{
    //Knows the win and lose conditions of the stage
    [SerializeField] private GameObject[] WinConditions;
    [SerializeField] private GameObject[] LoseConditions;
    [SerializeField] private GameObject GameEndPanel;

    [SerializeField] private GameObject UIWinPanel;
    [SerializeField] private GameObject UILosePanel;
    [SerializeField] private string NextSceneName;
    [SerializeField] private int SceneIndex = 0;

    private bool ConditionChecked = false;
    private void Update()
    {
        if (CheckLoseConditions() && !ConditionChecked)
        {
            ConditionChecked = true;
            //Level Lose actions
            GameEndPanel.SetActive(true);
            UILosePanel.SetActive(true);
            Time.timeScale = 0f;
            return;
        }
        if (CheckWinConditions() && !ConditionChecked)
        {
            ConditionChecked = true;
            //level win actions
            GameEndPanel.SetActive(true);
            UIWinPanel.SetActive(true);
            Time.timeScale = 0f;
            return;
        }
        
    }
    private bool CheckWinConditions()
    {
        foreach (GameObject condition in WinConditions)
        {
            if (condition.GetComponent<CCondition>().IsConditionAchieved() == false)
            {
                return false;
            }
        }
        return true;
    }
    private bool CheckLoseConditions()
    {
        foreach(GameObject condition in LoseConditions)
        {
            if(condition.GetComponent<CCondition>().IsConditionAchieved() == true)
            {
                return true;
            }
        }
        return false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        GameEndPanel.SetActive(false);
        EventManager.TransitionStart(SceneManager.GetActiveScene().name);
        

    }
    public void OnMainMenuClicked()
    {
        Time.timeScale = 1f;
        GameEndPanel.SetActive(false);
        EventManager.TransitionStart("MainMenu");
        
    }
    public void OnGoToNextScene()
    {
        Time.timeScale = 1f;
        if (PlayerPrefs.GetInt("LastLevelFinished")<=SceneIndex)
        {
            PlayerPrefs.SetInt("LastLevelFinished", SceneIndex);
        }
        GameEndPanel.SetActive(false);
        EventManager.TransitionStart(NextSceneName);
    }
}
