using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CResourceBuilding : CBuildingBase
{
    [SerializeField] private string ResourceType = "";
    [SerializeField] private GameObject AnimationsOnResourceGained;
    [SerializeField] private int ResourceIncrementAmount = 1;
    private int Morale = 50;
    private float UpdatedMaxTimeForProduction = 0;
    private float MoraleEffectPercentage;

    private void Start()
    {
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
