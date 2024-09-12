using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CTopRightPanel : MonoBehaviour
{
    [SerializeField] GameObject SettingsPopUp;
    [SerializeField] GameObject OptionsPopUp;
    public void ClosePopUp()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        EventManager.TransitionStart(SceneManager.GetActiveScene().name);
       
    }
    public void OnMainMenuClicked()
    {
        Time.timeScale = 1f;
        EventManager.TransitionStart("MainMenu");
    }
    public void OpenSettingsPopUp()
    {
        PauseGame();
        SettingsPopUp.SetActive(true);
        OptionsPopUp.SetActive(false);
    }
    public void OpenOptionsPopUp()
    {
        PauseGame();
        SettingsPopUp.SetActive(false);
        OptionsPopUp.SetActive(true);
    }
}
