using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Event/Set Flag")]
public class SetFlagEvent : DialogueEvent
{
    public string flagName;
    public bool value = true;

    public override int Execute()
    {
        GameFlags.Set(flagName, value);
        return 0;
    }
}