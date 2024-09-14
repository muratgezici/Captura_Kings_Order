using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class CMilitaryBuilding : CBuildingBase
{
    [SerializeField] private GameObject[] UnitsToProduce;
    [SerializeField] private Sprite[] UnitsToProduceSprites;
    [SerializeField] private GameObject ProducePoint;
    [SerializeField] private GameObject AnimationsOnArmyProduced;

    private int ProductionCount0 = 0;
    private int ProductionCount1 = 0;

    private float ProductionTimer = 0f;
    private float MaxProductionTimer = 0f;

    private int WhichToProduceFirst = -1;
    private bool IsProducingUnits = false;

    private int Morale = 50;
    private float UpdatedMaxTimeForProduction = 0;
    private float MoraleEffectPercentage;

    protected override void Start()
    {
        base.Start();
        OnEventEnable();
        MaxProductionTimer = MaxTimeForProduction;
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
    public void AddUnitToProductionLine(int index)
    {
        if(index == 0)
        {
            ProductionCount0++;
        }
        if (index == 1)
        {
            ProductionCount1++;
        }
    }
    public void RemoveUnitToProductionLine(int index)
    {
        if (index == 0 && ProductionCount0 > 0)
        {
            ProductionCount0--;
            if(ProductionCount0 == 0 && WhichToProduceFirst == 0)
            {
                ProductionTimer = 0;
                if (ProductionCount1 > 0)
                {
                    WhichToProduceFirst = 1;
                }
                else
                {
                    WhichToProduceFirst = -1;
                }
            }
        }
        if (index == 1 && ProductionCount1 > 0)
        {
            ProductionCount1--;
            if (ProductionCount1 == 0 && WhichToProduceFirst == 1)
            {
                ProductionTimer = 0;
                if(ProductionCount0 > 0)
                {
                    WhichToProduceFirst = 0;
                }
                else
                {
                    WhichToProduceFirst = -1;
                }
            }
        }
    }

    private void Update()
    {

        if(ProductionCount0 > 0 && !IsProducingUnits)
        {
            WhichToProduceFirst = 0;
            IsProducingUnits = true;
        }
        else if (ProductionCount1 > 0 && !IsProducingUnits)
        {
            WhichToProduceFirst = 1;
            IsProducingUnits = true;
        }
        else if(ProductionCount0 == 0 && ProductionCount1 == 0)
        {
            WhichToProduceFirst = -1;
            IsProducingUnits = false;
            return;
        }

        bool result_of_production = ProduceUnit(WhichToProduceFirst);
        if(result_of_production)
        {
            IsProducingUnits = false;
        }
        

    }
    public void ResetProduction()
    {
        ProductionCount0 = 0;
        ProductionCount1 = 0;
        ProductionTimer = 0;
        WhichToProduceFirst = -1;
        IsProducingUnits = false;

    }
    public bool ProduceUnit(int which_to_produce)
    {
        if (!IsProducingUnits)
        {
            ProductionTimer = 0;
            return false;
        }
        ProductionTimer += Time.deltaTime;
        EventManager.ArmyProductionUpdated(ProductionTimer, UpdatedMaxTimeForProduction, gameObject.GetComponent<CMilitaryBuilding>());
        //eventsysteme event yolla UIdaki production þeysini göster

        if (ProductionTimer > UpdatedMaxTimeForProduction)
        {
            ProductionTimer = 0;
            if (which_to_produce == 0)
            {
                if(ProductionCount0 > 0)
                {
                    ProductionCount0--;
                    GameObject unit = Instantiate(UnitsToProduce[0], ProducePoint.transform.position, Quaternion.identity);
                    unit.GetComponent<CSoldierUnitBase>().SetOwnedByColor(OwnedByColor);
                    float rand = Random.Range(-0.1f, 0.1f);
                    unit.transform.position += new Vector3(rand, 0, rand);
                    EventManager.ArmyProductionFinished(gameObject.GetComponent<CMilitaryBuilding>());
                    Debug.Log("produced");
                    if(ProductionCount0 == 0)
                    {
                        ProductionTimer = 0;
                        if (ProductionCount1 > 0)
                        {
                            WhichToProduceFirst = 1;
                        }
                        else
                        {
                            WhichToProduceFirst = -1;
                        }
                        return true;
                    }
                }
                
            }
            else if (which_to_produce == 1)
            {
                if (ProductionCount1 > 0)
                {
                    ProductionCount1--;
                    GameObject unit = Instantiate(UnitsToProduce[1], ProducePoint.transform.position, Quaternion.identity);
                    unit.GetComponent<CSoldierUnitBase>().SetOwnedByColor(OwnedByColor);
                    float rand = Random.Range(-0.1f,0.1f);
                    unit.transform.position += new Vector3(rand, 0, rand);
                    EventManager.ArmyProductionFinished(gameObject.GetComponent<CMilitaryBuilding>());
                    if (ProductionCount1 == 0)
                    {
                        ProductionTimer = 0;
                        if (ProductionCount0 > 0)
                        {
                            WhichToProduceFirst = 0;
                        }
                        else
                        {
                            WhichToProduceFirst = -1;
                        }
                        return true;
                    }
                }
                
            }
            ProductionTimer = 0;
            return false;
        }
        return false;
    }
    public void UpdateMorale(int value)
    {
        if (OwnedByColor != "blue")
        {
            return;
        }
        Morale = value;
        if (Morale > 50)
        {
            int temp_morale = Morale - 50;
            UpdatedMaxTimeForProduction = MaxTimeForProduction - MaxTimeForProduction * (0.2f * (temp_morale / 50f));
            MoraleEffectPercentage = (0.2f * (temp_morale / 50f));
        }
        else if (Morale < 50)
        {
            int temp_morale = 50 - Morale;
            UpdatedMaxTimeForProduction = MaxTimeForProduction + MaxTimeForProduction * (0.3f * (temp_morale / 50f));
            MoraleEffectPercentage = (0.3f * (temp_morale / 50f));
        }
        else
        {
            UpdatedMaxTimeForProduction = MaxTimeForProduction;
        }

        //0dan 50ye -%30a kadar düþ, 50 den 100 e +%20 ye kadar çýk
    }
 
    #region Getters
    public GameObject[] GetUnitsToProduce()
    {
        return UnitsToProduce;
    }
    public Sprite[] GetSoldierUnitSprites()
    {
        return UnitsToProduceSprites;
    }
    public float GetUpdatedMaxTimeForProduction()
    {
        return UpdatedMaxTimeForProduction;
    }
    public int GetProductionCount0()
    {
        return ProductionCount0;
    }
    public int GetProductionCount1()
    {
        return ProductionCount1;
    }
    public int GetWhichToProduceFirst()
    {
        return WhichToProduceFirst;
    }
    #endregion
}
