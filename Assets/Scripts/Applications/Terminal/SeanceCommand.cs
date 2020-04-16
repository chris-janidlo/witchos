using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeanceCommand : TerminalCommand
{
	public override IEnumerator Evaluate (TerminalApp term, string[] arguments)
	{
		if (arguments.Length == 1)
		{
			printUsage(term);
			yield break;
		}

		string name = String.Join(" ", arguments.Skip(1));

		term.PrintLine("connecting to spirit world...");
		yield return new WaitForSeconds(3);
		
		term.PrintLine("connection succeeded.");
		term.PrintLine("now echoing the laments of the dead. press escape to stop.");

		var laments = Seance.GetChants(name);

		yield return new WaitForSeconds(3);

		while (!term.SIGINT)
		{
			laments.MoveNext();
			term.PrintLine(laments.Current);

			// roll our own weird WaitForSeconds so that we can immediately break if escape key is pressed
			float timer = UnityEngine.Random.Range(.5f, 3);
			while (!term.SIGINT && timer >= 0)
			{
				yield return null;
				timer -= Time.deltaTime;
			}
		}
	}
}
