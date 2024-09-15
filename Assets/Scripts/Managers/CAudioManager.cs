using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAudioManager : MonoBehaviour
{
   [SerializeField]private AudioSource audioSource;
    void Start()
    {
        if (PlayerPrefs.GetFloat("MusicSettings") != 0)
        {
            audioSource.volume = PlayerPrefs.GetFloat("MusicSettings")/100f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetFloat("MusicSettings") != 0)
        {
            audioSource.volume = PlayerPrefs.GetFloat("MusicSettings")/100f;
        }
    }
}
