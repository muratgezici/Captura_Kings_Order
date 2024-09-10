using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBuildingBase : MonoBehaviour
{
    [SerializeField] protected string BuildingName = "";
    [SerializeField] protected string OwnedByColor = "None";
    [SerializeField] protected float MaxTimeForProduction = 0;
    [SerializeField] protected List<GameObject> UnitsInsideTile = new List<GameObject>();
    [SerializeField] protected float TimeCounter = 0;
}
