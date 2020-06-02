using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audiosbeci;
    public GameObject thislol;
    public GameObject controlsMenu;
    public GameObject mainMenu;

    public void SetVolume(float volume)
    {
        audiosbeci.SetFloat("volume", volume);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Controls()
    {
        thislol.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void Back()
    {
        thislol.SetActive(false);
        mainMenu.SetActive(true);
    }
}
