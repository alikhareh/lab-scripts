using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class SpectroDeviceManager : MonoBehaviour
{
    public PlayerMovementManager player;
    public Animator LCDAnimator, DeviceAnimator;
    public GameObject sample, blank; //sample in device
    
    public float sourceWaveLenght, I0, opticalBandWidth, pathLength, loadingTime;
    public bool isBlank;
    [HideInInspector]public float blankSolventLandaMax;
    public string measurementMode;
    public Transform samplePutPlace, blankPutPlace;

    public GameObject homePanel, resPanel, wavePanel, modePanel, bootPanel;

    public List<TextMeshProUGUI> SourceWaveLenghtTextMeshProUGUI, MeasurmentModeTextMeshProUGUI, IsBlankTextMeshProUGUI;
    public TextMeshProUGUI ErorrLogTextMeshProUguis, resTextmeshProUguis;
    public bool isOn, isOpen;

    void Update()
    {
        isOpen = DeviceAnimator.GetBool("isOpen");
        isOn = LCDAnimator.GetBool("isOn");
        
        foreach (var WVARIABLE in SourceWaveLenghtTextMeshProUGUI)
        {
            WVARIABLE.text = "len " + sourceWaveLenght + " nm";
        }
        foreach (var MVARIABLE in MeasurmentModeTextMeshProUGUI)
        {
            MVARIABLE.text = "Mode: " + measurementMode;
            
        }
        foreach (var IVARIABLE in IsBlankTextMeshProUGUI) 
        { 
            IVARIABLE.text = "Blank mode is " + isBlank.ToString();;
        }
    }

    //ALL SPECTRO ACTIONS

    public void DeviceOnAndOff()
    {
        if (isOn)
        {
            //isOn = false;
            LCDAnimator.SetBool("isOn", false);
            bootPanel.SetActive(false);
            resPanel.SetActive(false);
            wavePanel.SetActive(false);
            modePanel.SetActive(false);
            homePanel.SetActive(false);
        }
        else
        {
            LCDAnimator.SetBool("isOn", true);
            homePanel.SetActive(true);
            bootPanel.SetActive(true);
        }
        
    }
    public void GetSample()
    {
        sample = player.inHandObject;
        sample.transform.position = samplePutPlace.position;
        sample.transform.SetParent(null);
        sample.GetComponent<Rigidbody>().isKinematic = true;
        player.inHandObject = null;
        print("ali");
    }

    public void GetBlank()
    {
        blank = player.inHandObject.gameObject;
        blank.transform.position = blankPutPlace.position;
        blank.transform.SetParent(null);
        blank.GetComponent<Rigidbody>().isKinematic = true;
        player.inHandObject = null;
    }

    public void ChangeMode(string mode)
    {
        measurementMode = mode;
    }
    
    public void ChangeSourceWaveLenght(float value)
    {
        sourceWaveLenght += value;
        
        if (sourceWaveLenght > 900)
        {
            sourceWaveLenght = 900;
        }
        
        else if (sourceWaveLenght < 100)
        {
            sourceWaveLenght = 100;
        }
    }

    public void ChangeIsBlank()
    {
        isBlank = !isBlank;
    }

    public void Calculate()
    {
        
        if (!DeviceAnimator.GetBool("isOpen"))
        {
            if (blank != null && isBlank) 
            {
                resTextmeshProUguis.text += "\nZero Done";
                //blankSolventLandaMax = sample.solventLandaMax;
                print("maaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            }

            if (sample != null && sample.TryGetComponent<SpectroSampleManager>(out var sampleManager))
            {
                float A = sampleManager.molarAbsorptivity * pathLength * sampleManager.concentration;
                A += !(blankSolventLandaMax == sampleManager.solventLandaMax) ? 1f : 0f;
                switch (measurementMode) 
                { 
                    case "Transmittance": 
                        resTextmeshProUguis.text = Math.Pow(10, -A).ToString(); 
                        break;
                    case "Concentration":
                        break;
                    case "Asorbance":
                        resTextmeshProUguis.text = A.ToString();
                        break;
                }
            }
            
            homePanel.SetActive(false); 
            resPanel.SetActive(true);
        }
        else if(DeviceAnimator.GetBool("isOpen"))
        {
            ErorrLogTextMeshProUguis.text = "erorr : please close the device door";
        }
        else if(sample == null)
        {
            ErorrLogTextMeshProUguis.text = "erorr : please put sample in device";
        }
    }

    public void CloseAndOpenDoor()
    {
        DeviceAnimator.SetBool("isOpen", !isOpen);
    }
}
