using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Event/CheckDeviceSampleEvevnt")]
public class CheckDeviceSampleEvevnt : DialogueEvent
{
    public List<string> itemNameList;
    public override int Execute()
    {
        var deviceManager = GameObject.FindObjectOfType<SpectroDeviceManager>();
        
        if (deviceManager != null && deviceManager.sample != null)
        {
            string itemName = deviceManager.sample.name;
            Debug.Log("itemName: " + itemName);

            if (itemNameList != null && itemNameList.Contains(itemName))
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
        return 5;
    }
}