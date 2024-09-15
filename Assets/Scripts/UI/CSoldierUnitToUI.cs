using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSoldierUnitToUI : MonoBehaviour
{
    private List<GameObject> Soldiers = new List<GameObject>();
    private bool IsFortifyEnabled = false;
    [SerializeField] Sprite[] ButtonSprites;
    [SerializeField] Button FortifyButton;
    private void Update()
    {
        if(gameObject.activeSelf == true)
        {
            CheckNullSoldiers();
            if(Soldiers.Count == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void SetSoldiers(List<GameObject> _Soldiers)
    {
        FortifyButton.image.sprite = ButtonSprites[1];
        IsFortifyEnabled = false;
        Soldiers.Clear();
        foreach (GameObject soldier_ in GameObject.FindGameObjectsWithTag("SoldierUnit"))
        {
            if (soldier_.GetComponent<CSoldierUnitBase>().GetOwnedByColor() == "blue" && soldier_.GetComponent<CSoldierUnitManager>().GetIsSoldierUnitSelected())
            {
                Soldiers.Add(soldier_);
            }
        }
    }

    public void OnFortifyButtonClicked()
    {
        CheckNullSoldiers();
        IsFortifyEnabled = !IsFortifyEnabled;
        if (IsFortifyEnabled)
        {
            FortifyButton.image.sprite = ButtonSprites[0];
            SetSoldierForitfyState(IsFortifyEnabled);
        }
        else
        {
            FortifyButton.image.sprite = ButtonSprites[1];
            SetSoldierForitfyState(IsFortifyEnabled);
        }

    }
    public void CheckNullSoldiers()
    {
        for (int i = Soldiers.Count - 1; i >= 0; i--)
        {
            if (Soldiers[i] == null)
            {
                Soldiers.Remove(Soldiers[i]);
            }
        }

    }
    public void SetSoldierForitfyState(bool value)
    {
        foreach (GameObject soldier in Soldiers)
        {
            soldier.GetComponent<CSoldierUnitBase>().SetIsGarrisonEnabled(value);
        }
    }
}
