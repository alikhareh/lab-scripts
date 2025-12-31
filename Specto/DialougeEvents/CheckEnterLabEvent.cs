using System.Collections.Generic;
using UnityEngine;

public class CheckEnterLabEvent : MonoBehaviour
{
    public List<DialogueNode> dialogues;
    public SpectoAssistBrainManager assistBrainManager;
    public void Awake()
    {
        assistBrainManager.DialogueNodes = dialogues;
    }
}