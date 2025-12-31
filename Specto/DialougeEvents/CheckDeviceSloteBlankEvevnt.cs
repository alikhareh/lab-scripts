using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckDeviceBlankSloteEvent : MonoBehaviour
{
    public SpectoAssistBrainManager assistBrainManager;
    public List<DialogueNode> correctDialogueNodes, WrongDialogueNodes;
    public SpectroDeviceManager device;
    public List<SpectroSampleManager> correctSamples;
    public DialogueManager dialogueManager;
    public PlayerMovementManager player;
    
    public void Check()
    {
        if (device.blank == null && !dialogueManager.isShowing && player.inHandObject != null)
        {
            print("shit1");
            if (player.inHandObject.TryGetComponent<SpectroSampleManager>(out var blankManager) && correctSamples.Contains(blankManager))
            { 
                assistBrainManager.DialogueNodes.AddRange(correctDialogueNodes);
                print("shit2");
            }
            else
            {
                assistBrainManager.DialogueNodes.AddRange(WrongDialogueNodes);
                print("shit3");
            }
        }
    }
}