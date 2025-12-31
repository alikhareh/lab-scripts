using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Event/Debug Log")]
public class DebugLogEvent : DialogueEvent
{
    public string message;

    public override int Execute()
    {
        Debug.Log(message);
        return 0;
    }
}