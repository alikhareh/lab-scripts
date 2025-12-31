using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Event/CheckHandBlankEvevnt")]
public class CheckHandBlankEvevnt : DialogueEvent
{
    public List<string> itemNameList;
    public override int Execute()
    {
        var player = GameObject.FindObjectOfType<PlayerMovementManager>();
        //var manager = GameObject.FindObjectOfType<AssistBrainManager>();
        
        if (player != null && player.inHandObject != null)
        {
            string itemName = player.inHandObject.GetComponent<LabObjectManager>().name;
            Debug.Log("itemName: " + itemName);

            if (itemNameList != null && itemNameList.Contains(itemName))
            {
                return 6;
            }
            else
            {
                return 7;
            }
        }
        return 8;
    }
}