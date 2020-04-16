using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueNameCommand : TerminalCommand
{
	public override IEnumerator Evaluate (TerminalApp term, string[] arguments)
	{
		if (arguments.Length == 1)
		{
			term.PrintLine("usage: tn name");
			yield break;
		}

		yield return new WaitForSeconds(.5f); // pretend to process something

		string trueName = TrueName.FromName(String.Join("", arguments.Skip(1)));
		term.PrintLine(trueName);
	}
}
