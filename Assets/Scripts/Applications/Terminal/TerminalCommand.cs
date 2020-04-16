using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TerminalCommand : ScriptableObject
{
    public string Name, ArgumentList;

    [TextArea(5, 20)]
    public string HelpOutput;

    public abstract IEnumerator Evaluate (TerminalApp term, string[] arguments);

    protected void printUsage (TerminalApp term)
    {
        term.PrintLine($"usage: {Name} {ArgumentList}");
    }
}
