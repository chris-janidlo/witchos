using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCommand : TerminalCommand
{
	public override IEnumerator Evaluate (TerminalApp term, string[] arguments)
	{
		// TODO: allow for other argument styles like 'set a = b', 'set a=b', etc
		if (arguments.Length < 3)
		{
			term.PrintLine("usage: set name value");
			yield break;
		}

		string value = String.Join(" ", arguments.Skip(2));

		TerminalState.Instance.EnvironmentVariables[arguments[1]] = String.Join(" ", value);
		term.PrintLine(arguments[1] + " = " + value);
	}
}
