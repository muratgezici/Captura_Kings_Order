using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMainMenuActions : MonoBehaviour
{
    [SerializeField] private GameObject SettingsPanel;
    private bool IsSettingsPanelOpened = false;
    public void OnStartGameClicked()
    {
        EventManager.TransitionStart("UITestScene");
    }
    public void OnSettingsClicked()
    {
        IsSettingsPanelOpened = !IsSettingsPanelOpened;
        SettingsPanel.SetActive(IsSettingsPanelOpened);
    }
}
