using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CResourceBuilding : CBuildingBase
{
    [SerializeField] private string ResourceType = "";
    [SerializeField] private GameObject AnimationsOnResourceGained;

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
            EventManager.UpdateFoodAmount(1);
        }
        else if (ResourceType == "wood")
        {
            EventManager.UpdateWoodAmount(1);
        }
        else if(ResourceType == "morale")
        {
            EventManager.UpdateMoraleAmount(1);
        }
        else if (ResourceType == "iron")
        {
            EventManager.UpdateIronAmount(1);
        }
        else if (ResourceType == "coin")
        {
            EventManager.UpdateCoinAmount(1);
        }

    }
}
