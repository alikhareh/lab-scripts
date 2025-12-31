using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public DialogueNode nextNode;
    public List<DialogueEvent> onSelectEvents;
}