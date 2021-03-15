using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public abstract class TerminalCommand : ScriptableObject
    {
        public string Name, ArgumentList;

        [TextArea(5, 20)]
        public string HelpOutput;

        public abstract IEnumerator Evaluate (TerminalApp term, string[] arguments);

        // called if the terminal app is closed before evaluation is finished. override to provide functionality
        public virtual void CleanUpEarly (TerminalApp term) { }

        protected void printUsage (TerminalApp term)
        {
            term.PrintLine($"usage: {Name} {ArgumentList}");
        }
    }
}
