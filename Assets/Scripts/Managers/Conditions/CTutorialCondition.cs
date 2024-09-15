using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CTutorialCondition : CCondition
{
    [SerializeField] private string[] TutorialText;
    [SerializeField] private GameObject UITutorialPanel;
    [SerializeField] private GameObject UITutorialText;
    private int count = 0;

    private float TimeCounter = 0f;
    [SerializeField] private float MaxTime = 2f;
    private bool IsTimeCounterActive = true;
    protected override void Update()
    {
        base.Update();
        if (IsEnabled)
        {
            TimeCounter += Time.deltaTime;
            if (TimeCounter > MaxTime)
            {
                if (count == TutorialText.Length)
                {
                    IsEnabled = false;
                    ConditionAchieved = true;
                    gameObject.GetComponent<CCondition>().SetConditionAchieved(true);
                    gameObject.GetComponent<CCondition>().SetIsEnabled(false);
                }
                else
                {
                    UITutorialText.GetComponent<TextMeshProUGUI>().text = TutorialText[count];
                    UITutorialPanel.SetActive(true);
                    Time.timeScale = 0f;
                }
                

            }
        }
        
    }

    public void OnTutorialAcceptClicked()
    {
        if(count == 0)
        {
            MaxTime *= 3;
        }
        count++;
        IsTimeCounterActive = true;
        TimeCounter = 0;
        UITutorialPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
