using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CMilitaryBuildingToUI : MonoBehaviour
{
    private CMilitaryBuilding MilBuilding;
    [SerializeField] private CResourceManager ResourceManager;
    [SerializeField] private GameObject RightTopPanel;
    [SerializeField] private GameObject RightBottomPanel;
    [SerializeField] private GameObject CancelButton0;
    [SerializeField] private GameObject CancelButton1;

    [SerializeField] private TextMeshProUGUI LVLText;
    [SerializeField] private TextMeshProUGUI BuildingNameText;
    [SerializeField] private Image BuildingImage;
    [SerializeField] private Image ProductionTimerImage;
    [SerializeField] private TextMeshProUGUI ProductionLine1Text;
    [SerializeField] private TextMeshProUGUI ProductionLine2Text;
    [SerializeField] private TextMeshProUGUI SoldierUnitName1Text;
    [SerializeField] private TextMeshProUGUI SoldierUnitName2Text;

    [SerializeField] private GameObject UnitProductionArea1;
    [SerializeField] private GameObject UnitProductionArea2;
    [SerializeField] private Image SoldierUnit1Image;
    [SerializeField] private Image SoldierUnit2Image;
    [SerializeField] private TextMeshProUGUI UnitFood1Text;
    [SerializeField] private TextMeshProUGUI UnitFood2Text;
    [SerializeField] private TextMeshProUGUI UnitWood1Text;
    [SerializeField] private TextMeshProUGUI UnitWood2Text;
    [SerializeField] private TextMeshProUGUI UnitIron1Text;
    [SerializeField] private TextMeshProUGUI UnitIron2Text;
    [SerializeField] private TextMeshProUGUI UnitCoin1Text;
    [SerializeField] private TextMeshProUGUI UnitCoin2Text;

    private void Start()
    {
        OnEventEnable();
    }
    public void OnEventEnable()
    {
        EventManager.OnSceneReload += OnEventDisable;
        EventManager.OnArmyProductionUpdated += UpdateUIWithArmyProduction;
        EventManager.OnArmyProductionFinished += ArmyUnitProductionFinished;
    }
    public void OnEventDisable()
    {
        EventManager.OnSceneReload -= OnEventDisable;
        EventManager.OnArmyProductionUpdated -= UpdateUIWithArmyProduction;
        EventManager.OnArmyProductionFinished -= ArmyUnitProductionFinished;
    }
    public void SetBuilding(GameObject building)
    {
         if (building.GetComponent<CMilitaryBuilding>() != null)
         {
            MilBuilding = building.GetComponent<CMilitaryBuilding>();
            ProductionTimerImage.fillAmount = 0;
            UpdateResourceInfoPanel();
         }

    }
    public void UpdateResourceInfoPanel()
    {
        GameObject[] produceable_units = MilBuilding.GetUnitsToProduce();
        if(MilBuilding.GetOwnedByColor() != "blue")
        {
            RightTopPanel.SetActive(false);
            RightBottomPanel.SetActive(false);
        }
        else
        {
            RightTopPanel.SetActive(true);
            RightBottomPanel.SetActive(true);
        }
        
        
            LVLText.text = "Lvl " + MilBuilding.GetBuildingLevel();
            BuildingNameText.text = MilBuilding.GetBuildingName();
            BuildingImage.sprite = MilBuilding.GetImageSprite();
            if(MilBuilding.GetWhichToProduceFirst() == 0)
            {
                ProductionLine1Text.text = produceable_units[0].GetComponent<CSoldierUnitBase>().GetSoldierName() + ": " + MilBuilding.GetProductionCount0()+" (Currently)";
            }
            else
            {
                ProductionLine1Text.text = produceable_units[0].GetComponent<CSoldierUnitBase>().GetSoldierName() + ": " + MilBuilding.GetProductionCount0();
            }
            if (MilBuilding.GetProductionCount0() > 0)
            {
                CancelButton0.SetActive(true);
            }
            else
            {
                CancelButton0.SetActive(false);
            }
            
            SoldierUnitName1Text.text = produceable_units[0].GetComponent<CSoldierUnitBase>().GetSoldierName();
            SoldierUnit1Image.sprite = MilBuilding.GetSoldierUnitSprites()[0];
            UnitFood1Text.text = produceable_units[0].GetComponent<CSoldierUnitBase>().GetFoodCost() + "";
            UnitWood1Text.text = produceable_units[0].GetComponent<CSoldierUnitBase>().GetWoodCost() + "";
            UnitIron1Text.text = produceable_units[0].GetComponent<CSoldierUnitBase>().GetIronCost() + "";
            UnitCoin1Text.text = produceable_units[0].GetComponent<CSoldierUnitBase>().GetCoinCost() + "";
        
        if (produceable_units.Length > 1)
        {
            UnitProductionArea2.SetActive(true);
            if (MilBuilding.GetWhichToProduceFirst() == 1)
            {
                ProductionLine2Text.text = produceable_units[1].GetComponent<CSoldierUnitBase>().GetSoldierName() + ": " + MilBuilding.GetProductionCount1() + " (Currently)";
            }
            else
            {
                ProductionLine2Text.text = produceable_units[1].GetComponent<CSoldierUnitBase>().GetSoldierName() + ": " + MilBuilding.GetProductionCount1();
            }
            if (MilBuilding.GetProductionCount1() > 0)
            {
                CancelButton1.SetActive(true);
            }
            else
            {
                CancelButton1.SetActive(false);
            }
            SoldierUnitName2Text.text = produceable_units[1].GetComponent<CSoldierUnitBase>().GetSoldierName();
            SoldierUnit2Image.sprite = MilBuilding.GetSoldierUnitSprites()[1];
            UnitFood2Text.text = produceable_units[1].GetComponent<CSoldierUnitBase>().GetFoodCost() + "";
            UnitWood2Text.text = produceable_units[1].GetComponent<CSoldierUnitBase>().GetWoodCost() + "";
            UnitIron2Text.text = produceable_units[1].GetComponent<CSoldierUnitBase>().GetIronCost() + "";
            UnitCoin2Text.text = produceable_units[1].GetComponent<CSoldierUnitBase>().GetCoinCost() + "";
        }
        else
        {
            UnitProductionArea2.SetActive(false);
            ProductionLine2Text.text = "";
        }


    }

    public void UpdateUIWithArmyProduction(float timer, float max_timer, CMilitaryBuilding mil_script)
    {
        if(MilBuilding == mil_script)
        {
            
            float percentage = timer / max_timer;
            if(percentage == 1)
            {
                ProductionTimerImage.fillAmount = 0;
            }
            else
            {
                ProductionTimerImage.fillAmount = percentage;
            }
            
            UpdateResourceInfoPanel();
        }
    }
    public void ArmyUnitProductionFinished(CMilitaryBuilding mil_script)
    {
        if (MilBuilding == mil_script)
        {
            ProductionTimerImage.fillAmount = 0;
            if (MilBuilding.GetProductionCount0() <= 0)
            {

                CancelButton0.SetActive(false);
            }
            if (MilBuilding.GetProductionCount1() <= 0)
            {
                CancelButton1.SetActive(false);
            }
            UpdateResourceInfoPanel();
        }
    }
    public void OnIncreaseProductionAmount0()
    {
        GameObject[] produceable_units = MilBuilding.GetUnitsToProduce();
        int ResourceWood = ResourceManager.GetWoodAmount();
        int ResourceCoin = ResourceManager.GetCoinAmount();
        int ResourceIron = ResourceManager.GetIronAmount();
        int ResourceFood = ResourceManager.GetFoodAmount();
        int CurrentPop = ResourceManager.GetCurrentPopulationAmount();
        int MaxPop = ResourceManager.GetMaxPopulationAmount();

        //Buildingde ilgili birimi eðer yeterli kaynak ve nufüs yeri varsa 1 arttýr. 
        if (produceable_units[0].GetComponent<CSoldierUnitBase>().GetFoodCost() <= ResourceFood && produceable_units[0].GetComponent<CSoldierUnitBase>().GetWoodCost() <= ResourceWood && produceable_units[0].GetComponent<CSoldierUnitBase>().GetIronCost() <= ResourceIron && produceable_units[0].GetComponent<CSoldierUnitBase>().GetCoinCost() <= ResourceCoin)
        {
            if(CurrentPop < MaxPop)
            {
                EventManager.UpdatePopulationAmount(1);
                EventManager.UpdateCoinAmount(-produceable_units[0].GetComponent<CSoldierUnitBase>().GetCoinCost());
                EventManager.UpdateIronAmount(-produceable_units[0].GetComponent<CSoldierUnitBase>().GetIronCost());
                EventManager.UpdateFoodAmount(-produceable_units[0].GetComponent<CSoldierUnitBase>().GetFoodCost());
                EventManager.UpdateWoodAmount(-produceable_units[0].GetComponent<CSoldierUnitBase>().GetWoodCost());
                MilBuilding.AddUnitToProductionLine(0);
                UpdateResourceInfoPanel();
                CancelButton0.SetActive(true);

            }
        }
        

    }
    public void OnIncreaseProductionAmount1()
    {
        GameObject[] produceable_units = MilBuilding.GetUnitsToProduce();
        int ResourceWood = ResourceManager.GetWoodAmount();
        int ResourceCoin = ResourceManager.GetCoinAmount();
        int ResourceIron = ResourceManager.GetIronAmount();
        int ResourceFood = ResourceManager.GetFoodAmount();
        int CurrentPop = ResourceManager.GetCurrentPopulationAmount();
        int MaxPop = ResourceManager.GetMaxPopulationAmount();

        //Buildingde ilgili birimi eðer yeterli kaynak ve nufüs yeri varsa 1 arttýr. 
        if (produceable_units[1].GetComponent<CSoldierUnitBase>().GetFoodCost() <= ResourceFood && produceable_units[1].GetComponent<CSoldierUnitBase>().GetWoodCost() <= ResourceWood && produceable_units[1].GetComponent<CSoldierUnitBase>().GetIronCost() <= ResourceIron && produceable_units[1].GetComponent<CSoldierUnitBase>().GetCoinCost() <= ResourceCoin)
        {
            if (CurrentPop < MaxPop)
            {
                EventManager.UpdatePopulationAmount(1);
                EventManager.UpdateCoinAmount(-produceable_units[1].GetComponent<CSoldierUnitBase>().GetCoinCost());
                EventManager.UpdateIronAmount(-produceable_units[1].GetComponent<CSoldierUnitBase>().GetIronCost());
                EventManager.UpdateFoodAmount(-produceable_units[1].GetComponent<CSoldierUnitBase>().GetFoodCost());
                EventManager.UpdateWoodAmount(-produceable_units[1].GetComponent<CSoldierUnitBase>().GetWoodCost());
                MilBuilding.AddUnitToProductionLine(1);
                UpdateResourceInfoPanel();
                CancelButton1.SetActive(true);

            }
        }
        
    }
    public void OnDecreaseProductionAmount0()
    {
        GameObject[] produceable_units = MilBuilding.GetUnitsToProduce();
        EventManager.UpdatePopulationAmount(-1);
        EventManager.UpdateCoinAmount(produceable_units[0].GetComponent<CSoldierUnitBase>().GetCoinCost());
        EventManager.UpdateIronAmount(produceable_units[0].GetComponent<CSoldierUnitBase>().GetIronCost());
        EventManager.UpdateFoodAmount(produceable_units[0].GetComponent<CSoldierUnitBase>().GetFoodCost());
        EventManager.UpdateWoodAmount(produceable_units[0].GetComponent<CSoldierUnitBase>().GetWoodCost());
        MilBuilding.RemoveUnitToProductionLine(0);
        UpdateResourceInfoPanel();

        if (MilBuilding.GetProductionCount0() <= 0)
        {
            if(MilBuilding.GetProductionCount1() <= 0)
            {
                ProductionTimerImage.fillAmount = 0;
            }
            CancelButton0.SetActive(false);
        }

    }
    public void OnDecreaseProductionAmount1()
    {
        GameObject[] produceable_units = MilBuilding.GetUnitsToProduce();
        EventManager.UpdatePopulationAmount(-1);
        EventManager.UpdateCoinAmount(produceable_units[1].GetComponent<CSoldierUnitBase>().GetCoinCost());
        EventManager.UpdateIronAmount(produceable_units[1].GetComponent<CSoldierUnitBase>().GetIronCost());
        EventManager.UpdateFoodAmount(produceable_units[1].GetComponent<CSoldierUnitBase>().GetFoodCost());
        EventManager.UpdateWoodAmount(produceable_units[1].GetComponent<CSoldierUnitBase>().GetWoodCost());
        MilBuilding.RemoveUnitToProductionLine(1);
        UpdateResourceInfoPanel();

        if (MilBuilding.GetProductionCount1() <= 0)
        {
            if (MilBuilding.GetProductionCount1() <= 0)
            {
                ProductionTimerImage.fillAmount = 0;
            }
            CancelButton1.SetActive(false);
        }

    }

}
