using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CAllEnemiesDieCondition : CCondition
{
    protected override void Update()
    {
        base.Update();
        if (CheckIfAllEnemiesDied())
        {
            gameObject.GetComponent<CCondition>().SetConditionAchieved(true);
            gameObject.GetComponent<CCondition>().SetIsEnabled(false);
        }
        
    }
    public bool CheckIfAllEnemiesDied()
    {
        GameObject[] soldier_units = GameObject.FindGameObjectsWithTag("SoldierUnit");
        for (int i = 0; i < soldier_units.Length; i++)
        {
            if (soldier_units[i].GetComponent<CSoldierUnitBase>().GetOwnedByColor() != "blue")
            {
                return false;

            }

        }
        return true;
    }
}
