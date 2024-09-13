using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CClickAndGetInfoForBottomUI : MonoBehaviour
{
    [SerializeField] private GameObject BuildingPanel;
    [SerializeField] private GameObject SoldierUnitPanel;
    [SerializeField] private GameObject SelectedSoldier;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, layerMask))
            {
                if (SelectedSoldier != null)
                {
                    SelectedSoldier.GetComponent<CSoldierUnitManager>().SetIsSoldierUnitSelected(false);
                    Debug.Log("soldier: " + SelectedSoldier.GetComponent<CSoldierUnitManager>().GetIsSoldierUnitSelected());
                    SelectedSoldier = null;
                }

                if (hit.transform.gameObject.CompareTag("Building"))
                {

                    BuildingPanel.SetActive(true);
                    BuildingPanel.GetComponent<CBuildingInfoToUI>().SetBuilding(hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("SoldierUnit"))
                {

                    SelectedSoldier = hit.transform.gameObject;
                    SelectedSoldier.GetComponent<CSoldierUnitManager>().SetIsSoldierUnitSelected(true);
                    BuildingPanel.SetActive(false);
                }
                else
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        BuildingPanel.SetActive(false);
                    }
                }
            }
        }
    }
}
