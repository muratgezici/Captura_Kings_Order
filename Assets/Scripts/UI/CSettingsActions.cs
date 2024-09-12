using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSettingsActions : MonoBehaviour
{
    [SerializeField] private GameObject MusicSettingsObject;
    [SerializeField] private GameObject SoundEffectSettingsObject;

    private void Start()
    {
        if(PlayerPrefs.GetFloat("MusicSettings") != 0)
        {
            MusicSettingsObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicSettings");
        }
        if(PlayerPrefs.GetFloat("SoundEffectSettings") != 0)
        {
            SoundEffectSettingsObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SoundEffectSettings");
        }
        

    }
    public void OnMusicSettingsChanged()
    {
        float value = MusicSettingsObject.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("MusicSettings", value);
    }
    public void OnSoundEffectSettingsChanged()
    {
        float value = SoundEffectSettingsObject.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("SoundEffectSettings", value);
    }

}
