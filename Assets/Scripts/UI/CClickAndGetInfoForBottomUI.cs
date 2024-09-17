using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class CClickAndGetInfoForBottomUI : MonoBehaviour
{
    [SerializeField] private GameObject BuildingPanel;
    [SerializeField] private GameObject MilitaryBuildingPanel;
    [SerializeField] private GameObject SoldierUnitPanel;
    private GameObject SelectedSoldier;
    private List<GameObject> Soldiers = new List<GameObject>();
    private float DoubleClickTimer = 0f;
    private float DoubleClickMaxTime = 0.08f;


    private bool IsMouse0Clicked = false;
    private bool IsMouse0ClickUp = false;
    
    private bool IsHoldModeActive = false;

    private int ClickCount = 0;
    private Vector2 MousePosStartPoint;
    private Vector2 MousePosEndPoint;
    [SerializeField] RectTransform SelectionBox;

    private void SetAllVariablesToDefault()
    {
        DoubleClickTimer = 0f;
        IsMouse0Clicked = false;
        IsMouse0ClickUp = false;
        IsHoldModeActive = false;
        SelectionBox.sizeDelta = Vector2.zero;
        SelectionBox.gameObject.SetActive(false);
    }
    private void Update()
    {

        

        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var go in raycastResults)
                {
                    if (go.gameObject.activeSelf)
                    {
                        Debug.Log("onUI: "+ go.gameObject.name);
                        SetAllVariablesToDefault();
                        return;
                    }
                }
            }
            ClickCount++;
            IsMouse0Clicked = true;
            Debug.Log("Mousebtndown");
            MousePosStartPoint = Input.mousePosition;
            EmptySoldierList();
        }

        if (Input.GetMouseButton(0) && IsMouse0Clicked)
        {
            MousePosEndPoint = Input.mousePosition;
            if (Vector2.Distance(MousePosStartPoint, MousePosEndPoint) > 4)
            {
                IsHoldModeActive = true;
                SelectionBox.sizeDelta = Vector2.zero;
                SelectionBox.gameObject.SetActive(true);
            }
            if (IsHoldModeActive)
            {
                Debug.Log("entered holdd");
                IsMouse0ClickUp = false;
                ResizeSelectionBox();
            }
        }

        if (Input.GetMouseButtonUp(0) && IsMouse0Clicked)
        {
            if (IsHoldModeActive)
            {
                IsHoldModeActive = false;
                SetAllVariablesToDefault();
            }
            IsMouse0ClickUp = true;
            Debug.Log("Mouse btn up");
            IsMouse0Clicked = false;

        }

        

      

        
        if (ClickCount > 0 && !IsHoldModeActive && IsMouse0ClickUp && !IsMouse0Clicked)
        {

            
            Ray mousepos = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (ClickCount >= 2)
            {
                DoDoubleClickActions(mousepos);
                Debug.Log("Double click done");
                IsMouse0ClickUp = false;
                DoubleClickTimer = 0;
                ClickCount = 0;
                SetAllVariablesToDefault();
            }
            else if(ClickCount > 0)
            {
                Debug.Log("Single click done");
                DoSingleClickOperations(mousepos);
            }
          
            
        }
        if (ClickCount > 0)
        {
            DoubleClickTimer += Time.deltaTime;
            if (DoubleClickTimer > DoubleClickMaxTime)
            {
                Debug.Log("Double click disabled");
                IsMouse0ClickUp = false;
                DoubleClickTimer = 0;
                ClickCount = 0;
                SetAllVariablesToDefault();




            }
        }
    }
    public void EmptySoldierList()
    {
        for (int i = Soldiers.Count - 1; i >= 0; i--)
        {
            if (Soldiers[i] == null)
            {
                Soldiers.Remove(Soldiers[i]);
            }
        }
        
        foreach (GameObject soldier_ in GameObject.FindGameObjectsWithTag("SoldierUnit"))
        {
            if(soldier_.GetComponent<CSoldierUnitBase>().GetOwnedByColor() == "blue")
            {
                soldier_.GetComponent<CSoldierUnitManager>().SetIsSoldierUnitSelected(false);
                soldier_.GetComponent<CSoldierUnitBase>().SetIsGarrisonEnabled(false);
            }
        }
        SelectedSoldier = null;
        Soldiers.Clear();
    }
    public void DoDoubleClickActions(Ray pos)
    {
        SetAllVariablesToDefault();
        RaycastHit hit;
        int layerMask = 1 << 6;
        if (Physics.Raycast(pos, out hit, 100, layerMask))
        {
            GameObject[] soldier_units = GameObject.FindGameObjectsWithTag("SoldierUnit");
            Soldiers.Add(hit.transform.gameObject);
            foreach (GameObject soldier_unit in  soldier_units)
            {
                if(soldier_unit.GetComponent<CSoldierUnitBase>().GetOwnedByColor() == hit.transform.gameObject.GetComponent<CSoldierUnitBase>().GetOwnedByColor())
                {
                    if (soldier_unit.GetComponent<CSoldierUnitBase>().GetSoldierName() == hit.transform.gameObject.GetComponent<CSoldierUnitBase>().GetSoldierName())
                    {
                        soldier_unit.GetComponent<CSoldierUnitManager>().SetIsSoldierUnitSelected(true);
                        Soldiers.Add(soldier_unit);
                    }
                }
                
            }
            if (Soldiers.Count > 0)
            {
                BuildingPanel.SetActive(false);
                MilitaryBuildingPanel.SetActive(false);
                SoldierUnitPanel.SetActive(true);
                SoldierUnitPanel.GetComponent<CSoldierUnitToUI>().SetSoldiers(Soldiers);
            }
            else
            {
                SoldierUnitPanel.SetActive(false);
            }
        }
        
    }
    public void DoSingleClickOperations(Ray pos)
    {
        SetAllVariablesToDefault();
        RaycastHit hit;
        int layerMask = 1 << 8 | (1 << 9);
        layerMask = ~layerMask;
        if (Physics.Raycast(pos, out hit, 100, layerMask))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {

            }
            else
            {


                if (SelectedSoldier != null)
                {
                    SelectedSoldier.GetComponent<CSoldierUnitManager>().SetIsSoldierUnitSelected(false);
                    Debug.Log("soldier: " + SelectedSoldier.GetComponent<CSoldierUnitManager>().GetIsSoldierUnitSelected());
                    SelectedSoldier = null;
                }

                if (hit.transform.gameObject.CompareTag("Building"))
                {
                    MilitaryBuildingPanel.SetActive(false);
                    SoldierUnitPanel.SetActive(false);
                    BuildingPanel.SetActive(true);
                    BuildingPanel.GetComponent<CBuildingInfoToUI>().SetBuilding(hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("MilitaryBuilding"))
                {
                    BuildingPanel.SetActive(false);
                    SoldierUnitPanel.SetActive(false);
                    MilitaryBuildingPanel.SetActive(true);
                    MilitaryBuildingPanel.GetComponent<CMilitaryBuildingToUI>().SetBuilding(hit.transform.gameObject);
                    
                }
                else if (hit.transform.gameObject.CompareTag("SoldierUnit"))
                {
                    if (hit.transform.gameObject.GetComponent<CSoldierUnitBase>().GetOwnedByColor() == "blue")
                    {
                        BuildingPanel.SetActive(false);
                        MilitaryBuildingPanel.SetActive(false);
                        SoldierUnitPanel.SetActive(true);
                        //SoldierUnitPanel.GetComponent<CSoldierUnitToUI>().SetBuilding(hit.transform.gameObject);

                        SelectedSoldier = hit.transform.gameObject;

                        SelectedSoldier.GetComponent<CSoldierUnitManager>().SetIsSoldierUnitSelected(true);
                        List<GameObject> single_soldier = new List<GameObject>();
                        single_soldier.Add(SelectedSoldier);
                        SoldierUnitPanel.GetComponent<CSoldierUnitToUI>().SetSoldiers(single_soldier);
                    }
                    

                }
                else
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        BuildingPanel.SetActive(false);
                        MilitaryBuildingPanel.SetActive(false);
                        SoldierUnitPanel.SetActive(false);
                    }
                }
            }
        }


    }
    private void ResizeSelectionBox()
    {
        
        float width = Input.mousePosition.x - MousePosStartPoint.x;
        float height = Input.mousePosition.y - MousePosStartPoint.y;
        SelectionBox.anchoredPosition = MousePosStartPoint + new Vector2(width/2, height/2);
        SelectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        Bounds bounds = new Bounds(SelectionBox.anchoredPosition, SelectionBox.sizeDelta);
        GameObject[] soldier_units = GameObject.FindGameObjectsWithTag("SoldierUnit");
        for (int i = 0; i< soldier_units.Length; i++)
        {
            //if units under selectionbox and are team blue choose them
            if (UnitsInSelectionBox(Camera.main.WorldToScreenPoint(soldier_units[i].transform.position), bounds))
            {
                if (soldier_units[i].GetComponent<CSoldierUnitBase>().GetOwnedByColor() == "blue")
                {
                    Soldiers.Add(soldier_units[i]);
                    soldier_units[i].GetComponent<CSoldierUnitManager>().SetIsSoldierUnitSelected(true);
                    Debug.Log("Selected");

                }
            }
        }
        if(Soldiers.Count > 0)
        {
            BuildingPanel.SetActive(false);
            MilitaryBuildingPanel.SetActive(false);
            SoldierUnitPanel.SetActive(true);
            SoldierUnitPanel.GetComponent<CSoldierUnitToUI>().SetSoldiers(Soldiers);
        }
        else
        {
            SoldierUnitPanel.SetActive(false);
        }
    }
    private bool UnitsInSelectionBox(Vector2 Position, Bounds bounds)
    {
        return Position.x > bounds.min.x && Position.x < bounds.max.x && Position.y > bounds.min.y && Position.y < bounds.max.y;
    }
}
