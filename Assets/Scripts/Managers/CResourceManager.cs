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

    [SerializeField] private GameObject WoodText;
    [SerializeField] private GameObject CoinText;
    [SerializeField] private GameObject FoodText;
    [SerializeField] private GameObject IronText;
    [SerializeField] private GameObject MoraleText;

    private void Start()
    {
        EnableResourceManagement();
    }
    #region Event Manager Actions
    public void EnableResourceManagement()
    {
        EventManager.OnSceneReload += DisableResourceManagement;
        EventManager.OnUpdateWoodAmount += UpdateWoodAmountByValue;
        EventManager.OnUpdateCoinAmount += UpdateCoinAmountByValue;
        EventManager.OnUpdateFoodAmount += UpdateFoodAmountByValue;
        EventManager.OnUpdateIronAmount += UpdateIronAmountByValue;
        EventManager.OnUpdateMoraleAmount += UpdateMoraleAmountByValue;
    }
    public void DisableResourceManagement()
    {
        EventManager.OnUpdateWoodAmount -= UpdateWoodAmountByValue;
        EventManager.OnUpdateCoinAmount -= UpdateCoinAmountByValue;
        EventManager.OnUpdateFoodAmount -= UpdateFoodAmountByValue;
        EventManager.OnUpdateIronAmount -= UpdateIronAmountByValue;
        EventManager.OnUpdateMoraleAmount -= UpdateMoraleAmountByValue;
        EventManager.OnSceneReload -= DisableResourceManagement;
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
        }
        else if (MoraleAmount > 0 && amount < 0)
        {
            MoraleAmount += amount;
            MoraleText.GetComponent<TextMeshProUGUI>().text = "" + MoraleAmount + "/100";
            MoraleText.transform.GetChild(0).GetComponent<MMF_Player>().PlayFeedbacks();
        }

    }
    #endregion
}
