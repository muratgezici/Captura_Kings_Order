using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBuildingBase : MonoBehaviour
{
    //Colors: green, red, blue(player), yellow(neutral)
    [SerializeField] protected string BuildingName = "";
    [SerializeField] protected string OwnedByColor = "yellow";
    [SerializeField] protected int BuildingLevel = 1;
    [SerializeField] protected float MaxTimeForProduction = 0;
    [SerializeField] protected List<GameObject> UnitsInsideTile = new List<GameObject>();
    [SerializeField] protected float TimeCounter = 0;
    [SerializeField] protected GameObject[] ExtraParticles;
    [SerializeField] protected GameObject[] OtherTeamBuildings;
    [SerializeField] protected int PopulationAmount = 0;
    protected bool IsOwnedByPlayer = false;
    protected bool IsPopulationAdded = false;
    protected virtual void Start()
    {
        OnBuildingCapture(OwnedByColor);
        PlayExtraParticles();
    }

    public void PlayExtraParticles()
    {
        if (ExtraParticles.Length != 0)
        {
            if (OwnedByColor != "yellow")
            {
                foreach (GameObject obj in ExtraParticles)
                {
                    obj.GetComponent<ParticleSystem>().Play();
                }
            }
            else
            {
                foreach (GameObject obj in ExtraParticles)
                {
                    obj.GetComponent<ParticleSystem>().Stop();
                }
            }
        }
    }
    public void OnBuildingCapture(string team_color)
    {
        TimeCounter = 0;
        OwnedByColor = team_color;
        if(OwnedByColor == "yellow")
        {
            IsOwnedByPlayer = false;
            OtherTeamBuildings[1].SetActive(false);
            OtherTeamBuildings[2].SetActive(false);
            OtherTeamBuildings[3].SetActive(false);
            OtherTeamBuildings[0].SetActive(true);
            EventManager.TeamChangeAutomaticAnimations("yellow", gameObject);
        }
        else if (OwnedByColor == "blue")
        {
            IsOwnedByPlayer = true;
             OtherTeamBuildings[0].SetActive(false);
            OtherTeamBuildings[2].SetActive(false);
            OtherTeamBuildings[3].SetActive(false);
            OtherTeamBuildings[1].SetActive(true);
            EventManager.TeamChangeAutomaticAnimations("blue", gameObject);
            EventManager.UpdateMaxPopulationAmount(PopulationAmount);
        }
        else if (OwnedByColor == "red")
        {
            IsOwnedByPlayer = false;
            OtherTeamBuildings[1].SetActive(false);
            OtherTeamBuildings[2].SetActive(true);
            OtherTeamBuildings[3].SetActive(false);
            OtherTeamBuildings[0].SetActive(false);
            EventManager.TeamChangeAutomaticAnimations("red", gameObject);
        }
        else if (OwnedByColor == "green")
        {
            IsOwnedByPlayer = false;
            OtherTeamBuildings[1].SetActive(false);
            OtherTeamBuildings[0].SetActive(false);
            OtherTeamBuildings[2].SetActive(false);
            OtherTeamBuildings[3].SetActive(true);
            EventManager.TeamChangeAutomaticAnimations("green", gameObject);
        }
        PlayExtraParticles();
       
    }
    public void ChangeMaximumPopulation()
    {
        if(IsOwnedByPlayer && !IsPopulationAdded)
        {
            IsPopulationAdded = true;
            if(PopulationAmount != 0)
            {
                EventManager.UpdateMaxPopulationAmount(PopulationAmount);
            }
        }
        else if(!IsOwnedByPlayer && IsPopulationAdded)
        {
            IsPopulationAdded = false;
            if (PopulationAmount != 0)
            {
                EventManager.UpdateMaxPopulationAmount(-PopulationAmount);
            }
        }
    }
    #region Getters
    public string GetBuildingName()
    {
        return BuildingName;
    }
    public string GetOwnedByColor()
    {
        return OwnedByColor;
    }
    public int GetBuildingLevel()
    {
        return BuildingLevel;
    }
    public float GetMaxTimeForProduction()
    {
        return MaxTimeForProduction;
    }
    public int GetPopulation()
    {
        return PopulationAmount;
    }

    #endregion

}
