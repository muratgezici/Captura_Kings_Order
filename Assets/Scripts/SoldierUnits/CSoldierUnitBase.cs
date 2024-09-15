using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoldierUnitBase : MonoBehaviour
{
    [SerializeField] protected string BuildingName = "";
    [SerializeField] protected string OwnedByColor = "blue";
    [SerializeField] protected bool IsGarrisonEnabled = false;
    [SerializeField] protected GameObject[] OtherTeamUnits;
    [SerializeField] protected GameObject AnimationHandler;
    [SerializeField] protected CSoldierUnitCombat SoldierUnitCombat;
    [SerializeField] protected Sprite SoldierUnitSprite;
    [SerializeField] protected int FoodCost;
    [SerializeField] protected int WoodCost;
    [SerializeField] protected int IronCost;
    [SerializeField] protected int CoinCost;
    [SerializeField] protected float TimeToProduce;

    private void Start()
    {
        SetTeam();
    }
    public string GetOwnedByColor()
    {
        return OwnedByColor;
    }
    public string GetSoldierName()
    {
        return BuildingName;
    }
    public int GetFoodCost()
    {
        return FoodCost;
    }
    public int GetWoodCost()
    {
        return WoodCost;
    }
    public int GetIronCost()
    {
        return IronCost;
    }
    public int GetCoinCost()
    {
        return CoinCost;
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
    public void SetOwnedByColor(string color)
    {
        OwnedByColor = color;
        SetTeam();
    }
    public void GarrisonedInBuilding()
    {
        if(OwnedByColor == "blue")
        {
            EventManager.UpdatePopulationAmount(-1);
        }
        
        Destroy(gameObject);
    }
    public bool GetIsGarrisonEnabled()
    {
        return IsGarrisonEnabled;
    }
    public void SetIsGarrisonEnabled(bool value)
    {
        IsGarrisonEnabled = value;
    }
}
