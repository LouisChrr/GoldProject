using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    
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

}
