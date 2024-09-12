using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoldierUnitBase : MonoBehaviour
{
    [SerializeField] protected string BuildingName = "";
    [SerializeField] protected string OwnedByColor = "blue";
    [SerializeField] protected GameObject[] OtherTeamUnits;
    [SerializeField] protected GameObject AnimationHandler;
    [SerializeField] protected CSoldierUnitCombat SoldierUnitCombat;

    private void Start()
    {
        SetTeam();
    }
    public string GetOwnedByColor()
    {
        return OwnedByColor;
    }
    public void DecreaseHealth(float value)
    {
        SoldierUnitCombat.DecreaseHealth(value);
    }
    private void SetTeam()
    {
        if(OwnedByColor == "blue")
        {
            OtherTeamUnits[0].SetActive(true);
            OtherTeamUnits[1].SetActive(false);
            OtherTeamUnits[2].SetActive(false);
        }
        else if (OwnedByColor == "red")
        {
            OtherTeamUnits[0].SetActive(false);
            OtherTeamUnits[1].SetActive(true);
            OtherTeamUnits[2].SetActive(false);
        }
        else if (OwnedByColor == "green")
        {
            OtherTeamUnits[0].SetActive(false);
            OtherTeamUnits[1].SetActive(false);
            OtherTeamUnits[2].SetActive(true);
        }
    }
}
