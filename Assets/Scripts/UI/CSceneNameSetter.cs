using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSceneNameSetter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI LevelText;
    void Start()
    {
        string m_Scene = SceneManager.GetActiveScene().name;
        string numbersOnly = Regex.Replace(m_Scene, "[^0-9]", "");

        LevelText.text = "Level " + numbersOnly;


    }
}
