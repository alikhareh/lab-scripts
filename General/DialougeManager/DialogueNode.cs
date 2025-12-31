using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    [TextArea(3, 6)]
    public string text;

    public Sprite sprite;

    public List<DialogueChoice> choices;
    public List<DialogueEvent> onEnterEvents;
    public List<DialogueEvent> onExitEvents;
}