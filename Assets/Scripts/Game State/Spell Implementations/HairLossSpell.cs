using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class HairLossSpell : Spell
{
	public override SpellType Type => SpellType.HairLoss;

	int requiredMirrors => TimeState.Instance.GetTodaysMoonPhase().IsWaning()
		? 2
		: 1;

	public override Regex GetRegex ()
	{
		return new Regex
		(@"^
			.*\s+			# name, followed by space characters
			borealeo		# keyword
		$", REGEX_OPTIONS);
	}

	public override bool ConditionsAreMet (IList<string> incantation)
	{
		return
			MirrorState.Instance.NumberBroken() >= requiredMirrors &&
			TerminalState.Instance.GetEnvironmentVariable("aura") == "null";
	}

	public override IEnumerator CastBehavior (TerminalApp term, IList<string> incantation)
	{
		for (int i = 0; i < requiredMirrors; i++)
		{
			if (!MirrorState.Instance.TryConsumeMagic())
			{
				throw new Exception("unable to consume magic despite being told there were enough mirrors to consume");
			}
		}

		// fun stuff begins here

		term.PrintEmptyLine();

		yield return new WaitForSeconds(1.5f);

		for (int i = 0; i < UnityEngine.Random.Range(5, 10); i++)
		{
			term.PrintLine(randomAscii(UnityEngine.Random.Range(13, 37)));
			yield return null;
		}

		for (int i = 0; i < 3; i++)
		{
			term.PrintEmptyLine();
			yield return new WaitForSeconds(.3f);
		}

		yield return new WaitForSeconds(.4f);

		// end fun stuff

		castAt(getName(incantation));
	}

	string getName (IList<string> incantation)
	{
		return String.Join(" ", incantation.Take(incantation.Count() - 1));
	}
}