using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMainMenuActions : MonoBehaviour
{
    [SerializeField] private GameObject SettingsPanel;
    private bool IsSettingsPanelOpened = false;
    [SerializeField] private GameObject StartGameButton;
    [SerializeField] private GameObject ContinueButton;

    private void Start()
    {
        if (PlayerPrefs.GetInt("LastLevelFinished") > 0)
        {
            StartGameButton.SetActive(false);
            ContinueButton.SetActive(true);
        }
        else
        {
            StartGameButton.SetActive(true);
            ContinueButton.SetActive(false);
        }
    }
    public void OnStartGameClicked()
    {

        int levelindex = PlayerPrefs.GetInt("LastLevelFinished") + 1;
        if (levelindex > 3)
        {
            string level = "Level" + 3;
            EventManager.TransitionStart(level);
        }
        else
        {
            string level = "Level" + levelindex;
            EventManager.TransitionStart(level);
        }
        
    }
    public void OnSettingsClicked()
    {
        IsSettingsPanelOpened = !IsSettingsPanelOpened;
        SettingsPanel.SetActive(IsSettingsPanelOpened);
    }
}
