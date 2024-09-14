using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CResourceManager : MonoBehaviour
{
    private int WoodAmount = 0;
    private int CoinAmount = 0;
    private int FoodAmount = 0;
    private int IronAmount = 0;
    private int MoraleAmount = 50;
    private int MaxPopulationAmount = 15;
    private int CurrentPopulation = 0;

    [SerializeField] private GameObject WoodText;
    [SerializeField] private GameObject CoinText;
    [SerializeField] private GameObject FoodText;
    [SerializeField] private GameObject IronText;
    [SerializeField] private GameObject MoraleText;
    [SerializeField] private GameObject PopulationText;

    private void Start()
    {
        EnableResourceManagement();
    }
    #region Getters
    public int GetFoodAmount()
    {
        return FoodAmount;
    }
    public int GetCoinAmount() 
    {  
        return CoinAmount; 
    }
    public int GetWoodAmount()
    {
        return WoodAmount;
    }
    public int GetIronAmount()
    {
        return IronAmount;
    }
    public int GetCurrentPopulationAmount()
    {
        return CurrentPopulation;
    }
    public int GetMaxPopulationAmount()
    {
        return MaxPopulationAmount;
    }

    #endregion
    #region Event Manager Actions
    public void EnableResourceManagement()
    {
        EventManager.OnSceneReload += DisableResourceManagement;
        EventManager.OnUpdateWoodAmount += UpdateWoodAmountByValue;
        EventManager.OnUpdateCoinAmount += UpdateCoinAmountByValue;
        EventManager.OnUpdateFoodAmount += UpdateFoodAmountByValue;
        EventManager.OnUpdateIronAmount += UpdateIronAmountByValue;
        EventManager.OnUpdateMoraleAmount += UpdateMoraleAmountByValue;
        EventManager.OnUpdatePopulationAmount += UpdatePopulationAmountByValue;
        EventManager.OnUpdateMaxPopulationAmount += UpdateMaxPopulationAmountByValue;
    }
    public void DisableResourceManagement()
    {
        EventManager.OnUpdateWoodAmount -= UpdateWoodAmountByValue;
        EventManager.OnUpdateCoinAmount -= UpdateCoinAmountByValue;
        EventManager.OnUpdateFoodAmount -= UpdateFoodAmountByValue;
        EventManager.OnUpdateIronAmount -= UpdateIronAmountByValue;
        EventManager.OnUpdateMoraleAmount -= UpdateMoraleAmountByValue;
        EventManager.OnSceneReload -= DisableResourceManagement;
        EventManager.OnUpdatePopulationAmount -= UpdatePopulationAmountByValue;
        EventManager.OnUpdateMaxPopulationAmount -= UpdateMaxPopulationAmountByValue;
    }
    #endregion
    #region Update Resource Amounts
    public void UpdateWoodAmountByValue(int amount)
    {
        WoodAmount += amount;
        WoodText.GetComponent<TextMeshProUGUI>().text = "" + WoodAmount;
        WoodText.GetComponent<MMF_Player>().PlayFeedbacks();
    }
    public void UpdateCoinAmountByValue(int amount)
    {
        CoinAmount += amount;
        CoinText.GetComponent<TextMeshProUGUI>().text = "" + CoinAmount;
        CoinText.GetComponent<MMF_Player>().PlayFeedbacks();
    }
    public void UpdateFoodAmountByValue(int amount)
    {
        FoodAmount += amount;
        FoodText.GetComponent<TextMeshProUGUI>().text = "" + FoodAmount;
        FoodText.GetComponent<MMF_Player>().PlayFeedbacks();
    }
    public void UpdateIronAmountByValue(int amount)
    {
        IronAmount += amount;
        IronText.GetComponent<TextMeshProUGUI>().text = "" + IronAmount;
        IronText.GetComponent<MMF_Player>().PlayFeedbacks();
    }
    public void UpdateMoraleAmountByValue(int amount)
    {
        if(MoraleAmount < 100 && amount > 0)
        {
            MoraleAmount += amount;
            MoraleText.GetComponent<TextMeshProUGUI>().text = "" + MoraleAmount + "/100";
            MoraleText.GetComponent<MMF_Player>().PlayFeedbacks();
            EventManager.MoraleChanged(MoraleAmount);
        }
        else if (MoraleAmount > 0 && amount < 0)
        {
            MoraleAmount += amount;
            MoraleText.GetComponent<TextMeshProUGUI>().text = "" + MoraleAmount + "/100";
            MoraleText.transform.GetChild(0).GetComponent<MMF_Player>().PlayFeedbacks();
            EventManager.MoraleChanged(MoraleAmount);
        }

    }
    public void UpdatePopulationAmountByValue(int amount)
    {
        if (CurrentPopulation < MaxPopulationAmount && amount > 0)
        {
            CurrentPopulation += amount;
            PopulationText.GetComponent<TextMeshProUGUI>().text = "" + CurrentPopulation + "/"+ MaxPopulationAmount;
            PopulationText.GetComponent<MMF_Player>().PlayFeedbacks();
            
        }
        else if (amount < 0)
        {
            CurrentPopulation += amount;
            PopulationText.GetComponent<TextMeshProUGUI>().text = "" + CurrentPopulation + "/"+ MaxPopulationAmount;
            PopulationText.transform.GetChild(0).GetComponent<MMF_Player>().PlayFeedbacks();
            
        }

    }
    public void UpdateMaxPopulationAmountByValue(int amount)
    {
        
            MaxPopulationAmount += amount;
            PopulationText.GetComponent<TextMeshProUGUI>().text = "" + CurrentPopulation + "/" + MaxPopulationAmount;
            PopulationText.GetComponent<MMF_Player>().PlayFeedbacks();
    }
    #endregion
}
