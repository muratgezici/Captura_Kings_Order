using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCondition : MonoBehaviour
{
    [SerializeField] protected bool ConditionAchieved = false;
    [SerializeField] protected string ConditionName;
    [SerializeField] protected GameObject[] DependentConditions;
    [SerializeField] protected bool IsEnabled = false;
    public bool IsConditionAchieved()
    {
        return ConditionAchieved;   
    }
    public bool GetIsEnabled()
    {
        return IsEnabled;
    }
    protected virtual void Update()
    {
        if(!IsEnabled)
        {
            return;
        }
    }
    public void SetConditionAchieved(bool val)
    {
        ConditionAchieved = val;    
    }
    public void SetIsEnabled(bool val)
    {
        IsEnabled = val;
    }
}
