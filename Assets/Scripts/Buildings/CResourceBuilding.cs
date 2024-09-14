using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CResourceBuilding : CBuildingBase
{
    [SerializeField] private string ResourceType = "";
    [SerializeField] private GameObject AnimationsOnResourceGained;
    [SerializeField] private int ResourceIncrementAmount = 1;
    [SerializeField] private int WoodCostForUpgrade;
    [SerializeField] private int IronCostForUpgrade;
    [SerializeField] private int CoinCostForUpgrade;

    private int Morale = 50;
    private float UpdatedMaxTimeForProduction = 0;
    private float MoraleEffectPercentage;


    protected override void Start()
    {
        base.Start();
        OnEventEnable();
    }
    public void OnEventEnable()
    {
        EventManager.OnSceneReload += OnEventDisable;
        EventManager.OnMoraleChanged += UpdateMorale;
    }
    public void OnEventDisable()
    {
        EventManager.OnSceneReload -= OnEventDisable;
        EventManager.OnMoraleChanged -= UpdateMorale;
    }
    public void UpdateMorale(int value)
    {
        if(OwnedByColor != "blue")
        {
            return;
        }
        Morale = value;
        if(Morale > 50)
        {
            int temp_morale = Morale - 50;
            UpdatedMaxTimeForProduction = MaxTimeForProduction - MaxTimeForProduction * (0.2f * (temp_morale / 50f));
            MoraleEffectPercentage = (0.2f * (temp_morale / 50f));
        }
        else if (Morale < 50)
        {
            int temp_morale = 50-Morale;
            UpdatedMaxTimeForProduction = MaxTimeForProduction + MaxTimeForProduction * (0.3f * (temp_morale / 50f));
            MoraleEffectPercentage = (0.3f * (temp_morale / 50f));
        }
        else
        {
            UpdatedMaxTimeForProduction = MaxTimeForProduction;
        }
        
            //0dan 50ye -%30a kadar düþ, 50 den 100 e +%20 ye kadar çýk
    }
    private void Update()
    {
        ResourceGainAction();
        ChangeMaximumPopulation();
    }
    public void ResourceGainAction()
    {
        
        if (OwnedByColor == "blue")
        {
            TimeCounter += Time.deltaTime;
            if (TimeCounter > UpdatedMaxTimeForProduction)
            {
                TimeCounter = 0;
                AnimationsOnResourceGained.GetComponent<MMF_Player>().PlayFeedbacks();
                BroadcastMessageToResourceManager();
            }
        }
    }

    
    private void BroadcastMessageToResourceManager()
    {
        if (ResourceType == "grain")
        {
            EventManager.UpdateFoodAmount(ResourceIncrementAmount);
        }
        else if (ResourceType == "wood")
        {
            EventManager.UpdateWoodAmount(ResourceIncrementAmount);
        }
        else if(ResourceType == "morale")
        {
            EventManager.UpdateMoraleAmount(ResourceIncrementAmount);
        }
        else if (ResourceType == "iron")
        {
            EventManager.UpdateIronAmount(ResourceIncrementAmount);
        }
        else if (ResourceType == "coin")
        {
            EventManager.UpdateCoinAmount(ResourceIncrementAmount);
        }

    }
   
    public int GetWoodCostForUpgrade()
    {
        return WoodCostForUpgrade;
    }
    public int GetIronCostForUpgrade()
    {
        return IronCostForUpgrade;
    }
    public int GetCoinCostForUpgrade()
    {
        return CoinCostForUpgrade;
    }
    public string GetUpgradeText()
    {
        if(BuildingName == "Townhall")
        {
            return "Get: Population + 10";
        }
        else if (BuildingName == "Houses")
        {
            return "Get: Population + 15";
        }
        else if (BuildingName == "Market")
        {
            return "Get: Get +1 Coin Each Cycle";
        }
        else if (BuildingName == "Mine")
        {
            return "Get: Get +1 Iron Each Cycle";
        }
        else if (BuildingName == "Lumbermill")
        {
            return "Get: Get +1 Wood Each Cycle";
        }
        else if (BuildingName == "Tavern")
        {
            return "Get: Get +1 Morale Each Cycle";
        }
        else if (BuildingName == "Church")
        {
            return "Get: Get +1 Morale Each Cycle";
        }
        else if (BuildingName == "Shrine")
        {
            return "Get: Get +1 Morale Each Cycle";
        }
        else if (BuildingName == "Windmill")
        {
            return "Get: Get +1 Food Each Cycle";
        }
        else if (BuildingName == "Watermill")
        {
            return "Get: Get +1 Food Each Cycle";
        }
        return "";
    }
    public void OnBuildingUpgrade()
    {
        if(BuildingLevel < 5)
        {
            BuildingLevel++;
            float new_wood_cost = WoodCostForUpgrade / 5f + WoodCostForUpgrade;
            WoodCostForUpgrade = Mathf.CeilToInt(new_wood_cost);
            float new_iron_cost = IronCostForUpgrade / 5f + IronCostForUpgrade;
            IronCostForUpgrade = Mathf.CeilToInt(new_iron_cost);
            float new_coin_cost = CoinCostForUpgrade / 5f + CoinCostForUpgrade;
            CoinCostForUpgrade = Mathf.CeilToInt(new_coin_cost);
            if (BuildingName == "Townhall")
            {
                PopulationAmount += 10;
                EventManager.UpdateMaxPopulationAmount(10);
            }
            else if (BuildingName == "Houses")
            {
                PopulationAmount += 15;
                EventManager.UpdateMaxPopulationAmount(15);
            }
            else
            {
                ResourceIncrementAmount += 1;
            }
           
        }
        else
        {
            BuildingLevel = -1; //Max level
        }
        

        
    }
    #region Getters
    public string GetResourceType()
    {
        return ResourceType;
    }
    public int GetResourceIncrementAmount()
    {
        return ResourceIncrementAmount;
    }
    public int GetMorale()
    {
        return Morale;
    }
    public float GetMoralePercentage()
    {
        return MoraleEffectPercentage;
    }
    #endregion
}
