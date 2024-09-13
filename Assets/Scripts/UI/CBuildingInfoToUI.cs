using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CBuildingInfoToUI : MonoBehaviour
{
    private CResourceBuilding Building;
    private CMilitaryBuilding MilBuilding;
    [SerializeField] private TextMeshProUGUI LVLText;
    [SerializeField] private TextMeshProUGUI BuildingNameText;
    [SerializeField] private Image BuildingImage;
    [SerializeField] private TextMeshProUGUI ResourceTypeText;
    [SerializeField] private TextMeshProUGUI ProductionPerSecondText;
    [SerializeField] private TextMeshProUGUI ProductionExplanationText;
    [SerializeField] private TextMeshProUGUI UpgradeToLevelText;
    [SerializeField] private TextMeshProUGUI WhatToGetAfterUpgradeText;
    [SerializeField] private TextMeshProUGUI WoodCostText;
    [SerializeField] private TextMeshProUGUI IronCostText;
    [SerializeField] private TextMeshProUGUI GoldCostText;
    [SerializeField] private TextMeshProUGUI PopulationAmountText;

    [SerializeField] private GameObject PopulationPanel;
    [SerializeField] private GameObject RightBottomPanel;

   

    public void SetBuilding(GameObject building)
    {
        if(building.GetComponent<CResourceBuilding>() != null)
        {
            Building = building.GetComponent<CResourceBuilding>();
            UpdateResourceInfoPanel();
        }
        else if (building.GetComponent<CMilitaryBuilding>() != null)
        {
            MilBuilding = building.GetComponent<CMilitaryBuilding>();

        }

    }

    public void UpdateResourceInfoPanel()
    {
        if(Building.GetBuildingName() == "Houses" || Building.GetBuildingName() == "Townhall")
        {
            PopulationPanel.SetActive(true);
            PopulationAmountText.text = ""+Building.GetPopulation();
        }
        else
        {
            PopulationPanel.SetActive(false);
        }
        if(Building.GetOwnedByColor() != "blue")
        {
            RightBottomPanel.SetActive(false);
        }
        else
        {
            RightBottomPanel.SetActive(true);
        }

        LVLText.text = "Lvl "+Building.GetBuildingLevel()+"";
        BuildingNameText.text = ""+Building.GetBuildingName();
        ResourceTypeText.text = "Resource type: "+Building.GetResourceType();

        //add morale too!
        float production_per_sec = 0;
        float morale_percentage = 0;
        if (Building.GetMorale() > 50 && Building.GetOwnedByColor() == "blue")
        {
             production_per_sec = Building.GetResourceIncrementAmount() / (Building.GetMaxTimeForProduction() - Building.GetMaxTimeForProduction() * Building.GetMoralePercentage());
             morale_percentage = ((Building.GetMorale()-50) / 50f) * 20f;

        }
        else if (Building.GetMorale() < 50 && Building.GetOwnedByColor() == "blue")
        {
            production_per_sec = Building.GetResourceIncrementAmount() / (Building.GetMaxTimeForProduction() + Building.GetMaxTimeForProduction() * Building.GetMoralePercentage());
            morale_percentage = -((50-Building.GetMorale()) / 50f) * 30f;
        }
        else
        {
            if(Building.GetOwnedByColor() == "blue")
            {
                production_per_sec = Building.GetResourceIncrementAmount() / Building.GetMaxTimeForProduction();
            }
            
        }

        ProductionPerSecondText.text = "Production per second: " + production_per_sec.ToString("F2");
        ProductionExplanationText.text = Building.GetResourceIncrementAmount() + " (Production amount per cycle) / " + Building.GetMaxTimeForProduction() +
            " (produce in every " + Building.GetMaxTimeForProduction() + " seconds) x Morale modifier (" + morale_percentage.ToString("F") + "%) = " + production_per_sec.ToString("F2");
        UpgradeToLevelText.text = "Upgrade to lvl " + Building.GetBuildingLevel();


    }
}
