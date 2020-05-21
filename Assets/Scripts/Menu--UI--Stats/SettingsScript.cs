using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public AudioMixer audioMixer;

    public GameObject[] activatedShadows = new GameObject[3];
    public GameObject[] desactivatedShadows = new GameObject[3];

    private int min = -80;
    private int max = 0;
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetEffect(float effect)
    {
        audioMixer.SetFloat("effect", effect);
    }

    public void SetMusic(float music)
    {
        audioMixer.SetFloat("music", music);
    }
    
    public void VolumeActivated()
    {
        activatedShadows[0].SetActive(true);
        desactivatedShadows[0].SetActive(false);
        audioMixer.SetFloat("volume", max);
    }
    
    public void VolumeDesactivated()
    {
        activatedShadows[0].SetActive(false);
        desactivatedShadows[0].SetActive(true);
        audioMixer.SetFloat("volume", min);
    }
    
    public void MusicActivated()
    {
        activatedShadows[1].SetActive(true);
        desactivatedShadows[1].SetActive(false);
        audioMixer.SetFloat("music", max);
    }
    
    public void MusicDesactivated()
    {
        activatedShadows[1].SetActive(false);
        desactivatedShadows[1].SetActive(true);
        audioMixer.SetFloat("music", min);
    }
    
    public void EffectActivated()
    {
        activatedShadows[2].SetActive(true);
        desactivatedShadows[2].SetActive(false);
        audioMixer.SetFloat("effect", max);
    }
    
    public void EffectDesactivated()
    {
        activatedShadows[2].SetActive(false);
        desactivatedShadows[2].SetActive(true);
        audioMixer.SetFloat("effect", min);
    }
}
