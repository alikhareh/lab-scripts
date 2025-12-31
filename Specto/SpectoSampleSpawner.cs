using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class SpectoSampleSpawner : MonoBehaviour
{
        public TMP_InputField wavelengthText, cText, molarAbsText;
        public TMP_Dropdown container;

        public List<GameObject> prefab;
        private GameObject currentObj;
        
        public PlayerMovementManager playerMovementManager;

        public Transform hand;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void spawn()
    {
        currentObj = Instantiate(prefab[container.value], hand, false);
        SpectroSampleManager currentObjSampleManager = currentObj.GetComponent<SpectroSampleManager>();
        print(currentObjSampleManager);
        currentObjSampleManager.molarAbsorptivity = float.Parse(molarAbsText.text);
        currentObjSampleManager.bestWaveLenght = float.Parse(wavelengthText.text);
        currentObjSampleManager.concentration = float.Parse(cText.text);
    }

    public void init()
    {
        playerMovementManager.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMovementManager.enabled = true;
        Cursor.visible = false;
    }
}
