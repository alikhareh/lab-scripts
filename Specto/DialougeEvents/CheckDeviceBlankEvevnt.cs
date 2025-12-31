using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Event/CheckDeviceBlankEvent")]
public class CheckDeviceBlankEvent : DialogueEvent
{
    public List<string> itemNameList;
    public override int Execute()
    {
        var deviceManager = GameObject.FindObjectOfType<SpectroDeviceManager>();
        
        if (deviceManager != null && deviceManager.blank != null)
        {
            string itemName = deviceManager.blank.name;
            Debug.Log("itemName: " + itemName);

            if (itemNameList != null && itemNameList.Contains(itemName))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        return 2;
    }
}