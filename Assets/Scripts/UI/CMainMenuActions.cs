using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMainMenuActions : MonoBehaviour
{
    [SerializeField] private GameObject SettingsPanel;
    private bool IsSettingsPanelOpened = false;
    public void OnStartGameClicked()
    {
        EventManager.TransitionStart("Level1");
    }
    public void OnSettingsClicked()
    {
        IsSettingsPanelOpened = !IsSettingsPanelOpened;
        SettingsPanel.SetActive(IsSettingsPanelOpened);
    }
}
