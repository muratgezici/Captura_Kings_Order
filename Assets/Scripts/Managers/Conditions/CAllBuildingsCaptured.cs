using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAllBuildingsCaptured : CCondition
{
    protected override void Update()
    {
        base.Update();
        if (CheckIfAllBuildingsCaptured())
        {
            gameObject.GetComponent<CCondition>().SetConditionAchieved(true);
            gameObject.GetComponent<CCondition>().SetIsEnabled(false);
        }

    }
    public bool CheckIfAllBuildingsCaptured()
    {
        GameObject[] soldier_units = GameObject.FindGameObjectsWithTag("Building");
        for (int i = 0; i < soldier_units.Length; i++)
        {
            if(soldier_units[i].GetComponent<CBuildingBase>().GetOwnedByColor() != null)
            {
                if (soldier_units[i].GetComponent<CBuildingBase>().GetOwnedByColor() != "blue")
                {
                    return false;

                }
            }
            

        }
        return true;
    }
}
