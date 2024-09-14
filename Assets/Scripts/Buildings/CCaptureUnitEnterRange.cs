using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCaptureUnitEnterRange : MonoBehaviour
{
    [SerializeField] CBuildingCapture BuildingCapture;

    private void OnTriggerEnter(Collider other)
    {
        BuildingCapture.OnUnitEntersRange(other);
    }
    private void OnTriggerExit(Collider other)
    {
        BuildingCapture.OnUnitExitsRange(other);
    }
}
