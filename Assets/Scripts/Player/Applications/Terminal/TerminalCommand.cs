using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public abstract class TerminalCommand : ScriptableObject, IEquatable<TerminalCommand>
    {
        public string Name, ArgumentList;

        [TextArea(5, 20)]
        public string HelpOutput;

        public abstract IEnumerator Evaluate (ITerminal term, string[] arguments);

        // called if the terminal app is closed before evaluation is finished. override to provide functionality
        public virtual void CleanUpEarly (ITerminal term) { }

        protected void printUsage (ITerminal term)
        {
            term.PrintSingleLine($"usage: {Name} {ArgumentList}");
        }

        public bool Equals (TerminalCommand other)
        {
            // TODO: might not be sufficient
            return this == other;
        }
    }
}
