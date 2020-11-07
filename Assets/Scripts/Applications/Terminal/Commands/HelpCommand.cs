using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.WitchOS;

namespace WitchOS
{
    public class HelpCommand : TerminalCommand
    {
        public TerminalCommandValueList Commands;

        public override IEnumerator Evaluate (ITerminal term, string[] arguments)
        {
            if (arguments.Length == 1)
            {
                term.PrintSingleLine("available commands:");
                term.PrintSingleLine(" " + String.Join(" ", Commands.OrderBy(c => c.Name).Select(c => c.Name)));
                term.PrintSingleLine("enter 'help' followed by a command name to learn more");
            }
            else
            {
                string commandName = arguments[1];
                var command = Commands.FirstOrDefault(c => c.Name == commandName);

                if (command == null)
                {
                    term.PrintSingleLine($"help: can't find any command named '{commandName}'");
                }
                else
                {
                    term.PrintMultipleLines(command.HelpOutput);
                }
            }

            yield return null;
        }
    }
}
