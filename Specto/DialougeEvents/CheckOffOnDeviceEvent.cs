using System.Collections.Generic;
using UnityEngine;

public class CheckOffOnDeviceEvent : MonoBehaviour
{
    public List<DialogueNode> onCloseDialogues, onOpenDialogues, offOpenDialogues, offCloseDialogues, offWhileProsDialogues;
    public SpectoAssistBrainManager assistBrainManager;
    public SpectroDeviceManager deviceManager;
    private bool isOpen;
    public void Check()
    {
        isOpen = deviceManager.isOpen;
        
        if (deviceManager != null)
        {
            if (!deviceManager.isOn)
            {
                if (isOpen)
                {
                    assistBrainManager.DialogueNodes.AddRange(onOpenDialogues);
                }
                else if (!isOpen)
                {
                    assistBrainManager.DialogueNodes.AddRange(onCloseDialogues);
                }

                
            }
            else if (deviceManager.isOn)
            {
                if (!deviceManager.homePanel.activeSelf)
                {
                    assistBrainManager.DialogueNodes.AddRange(offWhileProsDialogues);
                }
                if (isOpen)
                {
                    assistBrainManager.DialogueNodes.AddRange(offOpenDialogues);
                }
                else if (!isOpen)
                {
                    assistBrainManager.DialogueNodes.AddRange(offCloseDialogues);
                }
                
            }
            
            
        }
    }
}