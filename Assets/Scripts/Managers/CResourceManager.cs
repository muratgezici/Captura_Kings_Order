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
    private int MoraleAmount = 0;

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
    }
    #endregion
    #region Update Resource Amounts
    public void UpdateWoodAmountByValue(int amount)
    {
        WoodAmount += amount;
        WoodText.GetComponent<TextMeshProUGUI>().text = "Wood: " + WoodAmount; 
    }
    public void UpdateCoinAmountByValue(int amount)
    {
        CoinAmount += amount;
        CoinText.GetComponent<TextMeshProUGUI>().text = "Coin: " + CoinAmount;
    }
    public void UpdateFoodAmountByValue(int amount)
    {
        FoodAmount += amount;
        FoodText.GetComponent<TextMeshProUGUI>().text = "Food: " + FoodAmount;
    }
    public void UpdateIronAmountByValue(int amount)
    {
        IronAmount += amount;
        IronText.GetComponent<TextMeshProUGUI>().text = "Iron: " + IronAmount;
    }
    public void UpdateMoraleAmountByValue(int amount)
    {
        MoraleAmount += amount;
        MoraleText.GetComponent<TextMeshProUGUI>().text = "Morale: " + MoraleAmount;
    }
    #endregion
}
