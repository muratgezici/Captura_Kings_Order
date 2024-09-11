using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CResourceBuilding : CBuildingBase
{
    [SerializeField] private string ResourceType = "";
    [SerializeField] private GameObject AnimationsOnResourceGained;
    [SerializeField] private int ResourceIncrementAmount = 1;

    private void Update()
    {
        ResourceGainAction();
    }
    public void ResourceGainAction()
    {
        
        if (OwnedByColor != "none")
        {
            TimeCounter += Time.deltaTime;
            if (TimeCounter > MaxTimeForProduction)
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
}
