using System;
using UnityEngine;

public class SpectroSampleManager : MonoBehaviour
{
    // epsilon * c * l = A
    public float molarAbsorptivity, concentration, bestWaveLenght, solventLandaMax;
    public string containerType;
    public LabObjectManager  objectManager;

    private void Start()
    {
        string bodyText = "Containertype: " + containerType +
        "\nMolarAbsorptivity: " + molarAbsorptivity.ToString() +
            "\nConcentration: " + concentration.ToString() +
            "\nMax wavelenght: " + bestWaveLenght.ToString();
                          
        objectManager.EditPopUp(bodyText);
    }
}