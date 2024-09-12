using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CTransitionHandler : MonoBehaviour
{
    [SerializeField] GameObject[] TransitionPanels;
    private Color initColor;
    private bool IsTransitionStarted = false;
    private float MaxTime = 0.6f;
    private float Timer = 0f;
    private string NextSceneName = "";
    private void Update()
    {
        if(IsTransitionStarted)
        {
            Timer += Time.deltaTime;
            if(Timer > MaxTime)
            {
                EventManager.SceneReload();
                SceneManager.LoadScene(NextSceneName);
                IsTransitionStarted = false;
            }

        }

    }
    private void Start()
    {
        initColor = TransitionPanels[0].GetComponent<Image>().color;
        EnableTransitionStart();
        TransitionFadeOut();
    }
    public void EnableTransitionStart()
    {
        EventManager.OnSceneReload += DisableTransitionStart;
        EventManager.OnTransitionStart += TransitionFadeIn;
    }
    public void DisableTransitionStart()
    {
        EventManager.OnSceneReload -= DisableTransitionStart;
        EventManager.OnTransitionStart -= TransitionFadeIn;
    }
    public void TransitionFadeIn(string next_scene)
    {
        NextSceneName = next_scene;
        IsTransitionStarted = true;
        GameObject lastpanel = null;
        foreach (var panel in TransitionPanels)
        {
            float rand = Random.Range(0.1f, 0.5f);
            //panel.GetComponent<Image>().color = initColor + new Color(rand, rand, rand);  
            panel.transform.GetChild(0).GetComponent<MMF_Player>().PlayFeedbacks();
            lastpanel = panel;
        }
        
    }
    public void TransitionFadeOut() 
    {
        foreach (var panel in TransitionPanels)
        {
            panel.transform.GetChild(1).GetComponent<MMF_Player>().PlayFeedbacks();
        }
    }
}
