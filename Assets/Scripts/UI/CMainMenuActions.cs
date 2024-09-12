using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMainMenuActions : MonoBehaviour
{
    public void OnStartGameClicked()
    {
        EventManager.TransitionStart("UITestScene");
    }
}
