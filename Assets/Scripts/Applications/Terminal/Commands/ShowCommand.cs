using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class ShowCommand : TerminalCommand
    {
        public EnvironmentVariableState EnvironmentVariableState;

        public override IEnumerator Evaluate (ITerminal term, string[] arguments)
        {
            var env = EnvironmentVariableState.EnvironmentVariables;
            if (arguments.Length == 1)
            {
                foreach (var pair in env)
                {
                    term.PrintSingleLine(pair.Key + " = " + pair.Value);
                    yield return null;
                }
            }
            else
            {
                foreach (var key in arguments.Skip(1))
                {
                    string result;
                    if (env.TryGetValue(key, out result))
                    {
                        term.PrintSingleLine(key + " = " + result);
                    }
                    else
                    {
                        term.PrintSingleLine(key + " not set");
                    }
                    yield return null;
                }
            }
        }
    }
}
