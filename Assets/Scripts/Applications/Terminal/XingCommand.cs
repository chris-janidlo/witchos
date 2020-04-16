using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XingCommand : TerminalCommand
{
	public override IEnumerator Evaluate (TerminalApp term, string[] arguments)
	{
		if (TerminalState.Instance.XingLock)
		{
			term.PrintLine("error - xing is already running in another window");
			yield break;
		}

		if (arguments.Length < 2)
		{
			term.PrintLine("usage: xing website|name");
			yield break;
		}

		string target = String.Join(" ", arguments.Skip(1));

		TerminalState.Instance.XingLock = true;

		term.PrintLine($"pointing imps toward {target}... (press ESC to cancel if desired. this may take some time)");

		float timer = UnityEngine.Random.Range(10, 20);
		while (!term.SIGINT && timer >= 0)
		{
			yield return null;
			timer -= Time.deltaTime;
		}

		TerminalState.Instance.XingTarget = target;
		term.PrintLine("done. now entering stability mode.");
		term.PrintLine("");
		term.PrintLine("press escape at any time to exit and RELEASE the imps from their current target.");

		while (!term.SIGINT)
		{
			yield return null;
		}

		TerminalState.Instance.XingLock = false;
	}
}
