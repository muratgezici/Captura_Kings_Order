using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CTransitionHandler : MonoBehaviour
{
    [SerializeField] GameObject[] TransitionPanels;
    private Color initColor;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TransitionFadeIn();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            TransitionFadeOut(); 
        }
    }
    private void Start()
    {
        initColor = TransitionPanels[0].GetComponent<Image>().color;
    }
    public void TransitionFadeIn()
    {
        foreach (var panel in TransitionPanels)
        {
            float rand = Random.Range(0.1f, 0.5f);
            panel.GetComponent<Image>().color = initColor + new Color(rand, rand, rand);  
            panel.transform.GetChild(0).GetComponent<MMF_Player>().PlayFeedbacks();
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
