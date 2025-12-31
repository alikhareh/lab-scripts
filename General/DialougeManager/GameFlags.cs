using System.Collections.Generic;

public static class GameFlags
{
    static Dictionary<string, bool> flags = new Dictionary<string, bool>();

    public static void Set(string key, bool value)
    {
        flags[key] = value;
    }

    public static bool Get(string key)
    {
        return flags.ContainsKey(key) && flags[key];
    }
}