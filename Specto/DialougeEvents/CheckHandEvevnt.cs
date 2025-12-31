using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckHandEvevnt : MonoBehaviour
{
    public SpectoAssistBrainManager assistBrainManager;
    public List<DialogueNode> sampleDialogueNodes, blankDialogueNodes;
    public PlayerMovementManager player;
    public bool IsHandFilled = false;
    
    private void Update()
    {
        if (player.inHandObject != null && IsHandFilled == false)
        {
            if (player.inHandObject.TryGetComponent(out SpectroSampleManager spectroSampleManager))
            {
                assistBrainManager.DialogueNodes = sampleDialogueNodes;
                IsHandFilled = true;
            }
            else if (player.inHandObject.TryGetComponent(out SpectoBlankManager spectoBlankManager))
            {
                assistBrainManager.DialogueNodes = blankDialogueNodes;
                IsHandFilled = true;
            }
            
        }
    }
}