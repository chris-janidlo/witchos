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
                term.PrintLine("available commands:");
                term.PrintLine(" " + String.Join(" ", Commands.OrderBy(c => c.Name).Select(c => c.Name)));
                term.PrintLine("enter 'help' followed by a command name to learn more");
            }
            else
            {
                string commandName = arguments[1];
                var command = Commands.FirstOrDefault(c => c.Name == commandName);

                if (command == null)
                {
                    term.PrintLine($"help: can't find any command named '{commandName}'");
                }
                else
                {
                    term.Print(command.HelpOutput);
                }
            }

            yield return null;
        }
    }
}
