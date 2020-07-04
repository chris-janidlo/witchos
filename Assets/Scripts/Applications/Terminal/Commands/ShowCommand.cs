using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class ShowCommand : TerminalCommand
    {
        public override IEnumerator Evaluate (TerminalApp term, string[] arguments)
        {
            var env = TerminalState.Instance.EnvironmentVariables;
            if (arguments.Length == 1)
            {
                foreach (var pair in env)
                {
                    term.PrintLine(pair.Key + " = " + pair.Value);
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
                        term.PrintLine(key + " = " + result);
                    }
                    else
                    {
                        term.PrintLine(key + " not set");
                    }
                    yield return null;
                }
            }
        }
    }
}
