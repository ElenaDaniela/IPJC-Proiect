using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    // Start is called before the first frame update
    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
}
