using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CResourceInfoPanelUI : MonoBehaviour
{
    [SerializeField] private Image ImageLeft;
    [SerializeField] private Image ImageRight;
    [SerializeField] private TextMeshProUGUI HeaderText;
    [SerializeField] private TextMeshProUGUI BuildingsText;
    [SerializeField] private TextMeshProUGUI InfoText;
    public void OnFoodHover(GameObject hovered)
    {
        gameObject.SetActive(true);
        transform.position = hovered.transform.position - new Vector3(0,140f,0);
        ImageLeft.sprite = hovered.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        ImageRight.sprite = hovered.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        HeaderText.text = "Food";
        BuildingsText.text = "Windmill\nWatermill";
        InfoText.text = "Lets you train army units. Each farm tile increases productivity of the mills.";
        HeaderText.color = hovered.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        BuildingsText.color = hovered.transform.GetChild(0).gameObject.GetComponent<Image>().color;

    }
    public void OnWoodHover(GameObject hovered)
    {
        gameObject.SetActive(true);
        transform.position = hovered.transform.position - new Vector3(0, 140f, 0);
        ImageLeft.sprite = hovered.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        ImageRight.sprite = hovered.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        HeaderText.text = "Wood";
        BuildingsText.text = "Lumbermill";
        InfoText.text = "Lets you train army units and upgrade buildings.";
        HeaderText.color = hovered.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        BuildingsText.color = hovered.transform.GetChild(0).gameObject.GetComponent<Image>().color;

    }
    public void OnIronHover(GameObject hovered)
    {
        gameObject.SetActive(true);
        transform.position = hovered.transform.position - new Vector3(0, 140f, 0);
        ImageLeft.sprite = hovered.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        ImageRight.sprite = hovered.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        HeaderText.text = "Iron";
        BuildingsText.text = "Mines";
        InfoText.text = "Lets you train army units and upgrade buildings.";
        HeaderText.color = hovered.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        BuildingsText.color = hovered.transform.GetChild(0).gameObject.GetComponent<Image>().color;
    }
    public void OnCoinHover(GameObject hovered)
    {
        gameObject.SetActive(true);
        transform.position = hovered.transform.position - new Vector3(0, 140f, 0);
        ImageLeft.sprite = hovered.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        ImageRight.sprite = hovered.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        HeaderText.text = "Coin";
        BuildingsText.text = "Houses\nTownhall\nMarket";
        InfoText.text = "Lets you train army units and upgrade buildings.";
        HeaderText.color = hovered.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        BuildingsText.color = hovered.transform.GetChild(0).gameObject.GetComponent<Image>().color;
    }
    public void OnMoraleHover(GameObject hovered)
    {
        gameObject.SetActive(true);
        transform.position = hovered.transform.position - new Vector3(0, 140f, 0);
        ImageLeft.sprite = hovered.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        ImageRight.sprite = hovered.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        HeaderText.text = "Morale";
        BuildingsText.text = "Church\nShrine\nTavern";
        InfoText.text = "If units die, morale decreases. Try to keep it above 50. Click on buildings/units to see effects.";
        HeaderText.color = hovered.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        BuildingsText.color = hovered.transform.GetChild(0).gameObject.GetComponent<Image>().color;
    }
    public void OnPopulationHover(GameObject hovered)
    {
        gameObject.SetActive(true);
        transform.position = hovered.transform.position - new Vector3(0, 140f, 0);
        ImageLeft.sprite = hovered.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        ImageRight.sprite = hovered.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        HeaderText.text = "Pops";
        BuildingsText.text = "Houses\nTownhall";
        InfoText.text = "Lets you train more army units. If full, you cannot train units anymore.";
        HeaderText.color = hovered.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        BuildingsText.color = hovered.transform.GetChild(0).gameObject.GetComponent<Image>().color;
    }
    public void OnExit()
    {
        gameObject.SetActive(false);
    }
}
