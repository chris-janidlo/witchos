using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class SetCommand : TerminalCommand
    {
        public EnvironmentVariableState EnvironmentVariableState;

        public override IEnumerator Evaluate (ITerminal term, string[] arguments)
        {
            // TODO: allow for other argument styles like 'set a = b', 'set a=b', etc
            if (arguments.Length < 3)
            {
                printUsage(term);
                yield break;
            }

            string value = String.Join(" ", arguments.Skip(2));

            EnvironmentVariableState.EnvironmentVariables[arguments[1]] = String.Join(" ", value);
            term.PrintLine(arguments[1] + " = " + value);
        }
    }
}
