using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBuildingCapture : MonoBehaviour
{
    private string OwnedByColor = "";
    [SerializeField] private CBuildingBase BuildingBase;
    [SerializeField] private int UnitsRequiredForCapture = 5;
    [SerializeField] private List<GameObject> UnitsInRange = new List<GameObject>();
    [SerializeField] private List<GameObject> SameColorUnitsInRange = new List<GameObject>();
    //How capture works:
    //If neutral: if units enter collision zone units required to capture decreases, color of last entered unit will be color of the building after required units reaches 0.
    //if building captured, required units for capture set to 1.
    //If same color unit enters, units required for capture increases until it reaches 10. max garrison is 10.
    //if building captured and there are same color units near the building, other teams cannot capture until all the units in range exits/dies. 

    private void Start()
    {
        OwnedByColor = gameObject.transform.parent.GetComponent<CBuildingBase>().GetOwnedByColor();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SoldierUnit"))
        {
            OnUnitEnterBuilding(other.gameObject);
        }
       
    }
    public void OnUnitEntersRange(Collider other)
    {
        if (other.gameObject.CompareTag("SoldierUnit"))
        {
            UnitsInRange.Add(other.gameObject);
        }
        
        
    }
    public void OnUnitExitsRange(Collider other)
    {
        if (other.gameObject.CompareTag("SoldierUnit"))
        {
            UnitsInRange.Remove(other.gameObject);
        }
        
        
    }
    public void OnUnitEnterBuilding(GameObject soldier_unit)
    {
        if (!soldier_unit.GetComponent<CSoldierUnitBase>().GetIsGarrisonEnabled())
        {
            return;
        }
        string unit_color = soldier_unit.GetComponent<CSoldierUnitBase>().GetOwnedByColor();
        if (soldier_unit.GetComponent<CSoldierUnitBase>().GetOwnedByColor() == OwnedByColor)
        {
            if(UnitsRequiredForCapture < 10)
            {
                UnitsRequiredForCapture++;
                BuildingBase.SetGarrisonAmount(UnitsRequiredForCapture);
                EventManager.UICanvasNeedsUpdate();
                soldier_unit.GetComponent<CSoldierUnitBase>().GarrisonedInBuilding();
            }
        }
        else
        {
            SetSameColorUnitsInRange();
            if (UnitsRequiredForCapture >= 0 && SameColorUnitsInRange.Count == 0)
            {
                
               
                UnitsRequiredForCapture--;
                BuildingBase.SetGarrisonAmount(UnitsRequiredForCapture);
                EventManager.UICanvasNeedsUpdate();
                if (UnitsRequiredForCapture < 0)
                {
                    OwnedByColor = unit_color;
                    BuildingBase.OnBuildingCapture(unit_color);
                    UnitsRequiredForCapture = 1;
                    BuildingBase.SetGarrisonAmount(UnitsRequiredForCapture);
                    

                }
                soldier_unit.GetComponent<CSoldierUnitBase>().GarrisonedInBuilding();
            }
            else
            {
                //same color units in range, cannot capture!
            }
        }
        
    }

    public void SetSameColorUnitsInRange()
    {
        for (int i = UnitsInRange.Count - 1; i >= 0; i--)
        {
            if (UnitsInRange[i] == null)
            {
                UnitsInRange.Remove(UnitsInRange[i]);
            }
        }

        SameColorUnitsInRange.Clear();
        foreach (GameObject unit in UnitsInRange)
        {
            if (unit.GetComponent<CSoldierUnitBase>().GetOwnedByColor() == OwnedByColor)
            {
                SameColorUnitsInRange.Add(unit);
            }
        }
    }
    
}
