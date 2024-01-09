using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class Options : MonoBehaviour
{

    public AudioMixer volumeLevel;
    public TextMeshProUGUI sliderInfo;

    public AudioMixer sfxLevel;
    public TextMeshProUGUI sliderInfo2;


    public void MusicVolume(float volume)
    {
        volumeLevel.SetFloat("MainMixer", volume);
        sliderInfo.text = Mathf.Floor(Mathf.Abs((volume / 80f * 100f) + 100f)) + " %";
    }

    public void SFXVolume(float volume)
    {
        sfxLevel.SetFloat("SFXMixer", volume);
        sliderInfo2.text = Mathf.Floor(Mathf.Abs((volume / 80f * 100f) + 100f)) + " %";
    }

    public void FullScreen (bool fullScreentoggle)
    {
        Screen.fullScreen = fullScreentoggle;
    }
}
