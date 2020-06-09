using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
public class HelpCommand : TerminalCommand
{
	public override IEnumerator Evaluate (TerminalApp term, string[] arguments)
	{
		if (arguments.Length == 1)
		{
			term.PrintLine("available commands:");
			term.PrintLine(" " + String.Join(" ", term.Commands.OrderBy(c => c.Name).Select(c => c.Name)));
			term.PrintLine("enter 'help' followed by a command name to learn more");
		}
		else
		{
			string com = arguments[1];
			if (term.CommandDict.ContainsKey(com))
			{
				term.Print(term.CommandDict[com].HelpOutput);
			}
			else
			{
				term.PrintLine($"help: can't find any command named '{com}'");
			}
		}

		yield return null;
	}
}
}
