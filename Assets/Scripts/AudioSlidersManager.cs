using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlidersManager : MonoBehaviour
{

    public Slider[] sliders;

    // Start is called before the first frame update
    void Start()
    {
        sliders[0].value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sliders[1].value = PlayerPrefs.GetFloat("SFXVolume", 0.8f);
        sliders[2].value = PlayerPrefs.GetFloat("VoiceVolume", 0.8f);
    }

    public void MusicUpdate(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    public void SFXUpdate(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
    public void VoiceUpdate(float value)
    {
        PlayerPrefs.SetFloat("VoiceVolume", value);
    }

}
