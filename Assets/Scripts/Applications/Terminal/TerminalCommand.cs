using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TerminalCommand : ScriptableObject
{
    public string Name;

    [TextArea(5, 20)]
    public string HelpOutput;

    public abstract IEnumerator Evaluate (TerminalApp term, string[] arguments);
}
