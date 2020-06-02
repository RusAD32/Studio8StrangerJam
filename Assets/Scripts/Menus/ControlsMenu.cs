using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    public GameObject thislol;
    public GameObject settingsMenu;


    public void Back()
    {
        thislol.SetActive(false);
        settingsMenu.SetActive(true);
    }
}
