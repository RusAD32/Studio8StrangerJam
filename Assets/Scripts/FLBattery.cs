using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLBattery : MonoBehaviour
{
    //for to toggle the battery
    public FlashLightToggle flashlightbatteries;

    //to see if the flashlight is on/off;
    public GameObject onbt;
    public GameObject offbt;

    //to disable the cubes when battery is lower than their value
    public GameObject EIGHTbt;
    public GameObject SIXbt;
    public GameObject FOURbt;
    public GameObject TWObt;
    public GameObject LAST;

    // Start is called before the first frame update
    void Start()
    {
        onbt.SetActive(false);
        offbt.SetActive(true);
        EIGHTbt.SetActive(true);
        SIXbt.SetActive(true);
        FOURbt.SetActive(true);
        TWObt.SetActive(true);
        LAST.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //toggle on/off
        if (flashlightbatteries.isOpened)
        {
            onbt.SetActive(true);
            offbt.SetActive(false);
        }
        else if (!flashlightbatteries.isOpened)
        {
            onbt.SetActive(false);
            offbt.SetActive(true);
        }

        //battery
        if (flashlightbatteries.battery <= 10)
        {
            EIGHTbt.SetActive(true);
            SIXbt.SetActive(true);
            FOURbt.SetActive(true);
            TWObt.SetActive(true);
            LAST.SetActive(true);
        }
        if (flashlightbatteries.battery < 8)
        {
            EIGHTbt.SetActive(false);
        }
        if (flashlightbatteries.battery < 6)
        {
            EIGHTbt.SetActive(false);
            SIXbt.SetActive(false);
        }
        if (flashlightbatteries.battery < 4)
        {
            EIGHTbt.SetActive(false);
            SIXbt.SetActive(false);
            FOURbt.SetActive(false);
        }
        if (flashlightbatteries.battery < 2)
        {
            EIGHTbt.SetActive(false);
            SIXbt.SetActive(false);
            FOURbt.SetActive(false);
            TWObt.SetActive(false);
        }
        if (flashlightbatteries.battery <= 0)
        {
            EIGHTbt.SetActive(false);
            SIXbt.SetActive(false);
            FOURbt.SetActive(false);
            TWObt.SetActive(false);
            LAST.SetActive(false);
        }
    }
}
