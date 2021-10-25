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
        sliders[0].value = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        sliders[1].value = PlayerPrefs.GetFloat("SFXVolume", 0.3f);
        sliders[2].value = PlayerPrefs.GetFloat("VoiceVolume", 0.8f);
        MusicUpdate(sliders[0].value);
        SFXUpdate(sliders[1].value);
        VoiceUpdate(sliders[2].value);
    }

    public void MusicUpdate(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        AudioManager.one.SetVolume("music", value);

        if (MenuManager.music)
        {
            MenuManager.music.volume = value;
        }
    }
    public void SFXUpdate(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        AudioManager.one.SetVolume("sfx", value);
    }
    public void VoiceUpdate(float value)
    {
        PlayerPrefs.SetFloat("VoiceVolume", value);
        AudioManager.one.SetVolume("voice", value);
    }

}
