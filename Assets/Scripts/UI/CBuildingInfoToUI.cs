using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CBuildingInfoToUI : MonoBehaviour
{
    [SerializeField] private CResourceManager ResourceManager;
    private CResourceBuilding Building;
    [SerializeField] private TextMeshProUGUI LVLText;
    [SerializeField] private TextMeshProUGUI GarrisonText;
    [SerializeField] private TextMeshProUGUI TeamColorText;
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
    [SerializeField] private MMF_Player OnSuccesfullUpgradePlay;

    private void Start()
    {
        OnEventEnable();
    }
    public void OnEventEnable()
    {
        EventManager.OnSceneReload += OnEventDisable;
        EventManager.OnUICanvasNeedsUpdate += UpdateResourceInfoPanel;
    }
    public void OnEventDisable()
    {
        EventManager.OnSceneReload -= OnEventDisable;
        EventManager.OnUICanvasNeedsUpdate -= UpdateResourceInfoPanel;
    }
    public void SetBuilding(GameObject building)
    {
        if(building.GetComponent<CResourceBuilding>() != null)
        {
            Building = building.GetComponent<CResourceBuilding>();
            UpdateResourceInfoPanel();
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
        if (Building.GetBuildingLevel() == -1)
        {
            LVLText.text = "Max Level";
        }
        else
        {
            LVLText.text = "Lvl " + Building.GetBuildingLevel() + "";
        }
        if(Building.GetOwnedByColor() == "yellow")
        {
            TeamColorText.text = "Team: " + "Neutral";
        }
        else
        {
            TeamColorText.text = "Team: " + Building.GetOwnedByColor();
        }
        
        GarrisonText.text = "(Units Inside:" + Building.GetGarrisonAmount() + ")";
        BuildingNameText.text = ""+Building.GetBuildingName();
        BuildingImage.sprite = Building.GetImageSprite();
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
        if(Building.GetBuildingLevel() == -1)
        {
            UpgradeToLevelText.text = "Max Level Reached!";
            WhatToGetAfterUpgradeText.text = "";
            WoodCostText.transform.parent.gameObject.SetActive(false);
            GoldCostText.transform.parent.gameObject.SetActive(false);
            IronCostText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            int level = Building.GetBuildingLevel() + 1;
            UpgradeToLevelText.text = "Upgrade to lvl " + (level);
            WhatToGetAfterUpgradeText.text = Building.GetUpgradeText();
            WoodCostText.text = ""+Building.GetWoodCostForUpgrade();
            GoldCostText.text =""+Building.GetCoinCostForUpgrade();
            IronCostText.text =""+Building.GetIronCostForUpgrade();

            WoodCostText.transform.parent.gameObject.SetActive(true);
            GoldCostText.transform.parent.gameObject.SetActive(true);
            IronCostText.transform.parent.gameObject.SetActive(true);
        }
        


    }

    public void OnUpgradeButtonClicked()
    {
        if (Building.GetBuildingLevel() == -1)
        {
            return;
        }
        int ResourceWood = ResourceManager.GetWoodAmount();
        int ResourceCoin = ResourceManager.GetCoinAmount();
        int ResourceIron = ResourceManager.GetIronAmount();
        if(Building.GetCoinCostForUpgrade() < ResourceCoin && Building.GetWoodCostForUpgrade() < ResourceWood && Building.GetIronCostForUpgrade() < ResourceIron)
        {
            EventManager.UpdateWoodAmount(-Building.GetWoodCostForUpgrade());
            EventManager.UpdateIronAmount(-Building.GetIronCostForUpgrade());
            EventManager.UpdateCoinAmount(-Building.GetCoinCostForUpgrade());

            Building.OnBuildingUpgrade();
            UpdateResourceInfoPanel();
            OnSuccesfullUpgradePlay.PlayFeedbacks();
        }
        


    }
}
