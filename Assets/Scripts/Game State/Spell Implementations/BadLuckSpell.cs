using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class BadLuckSpell : Spell
{
	public override SpellType Type => SpellType.BadLuck;

	public override Regex GetRegex ()
	{
		return new Regex
		(@"^
			malam			# first keyword
			\s+.+\s+		# name, surrounded by space characters
			fortunam		# second keyword
			\s+\w+\s+		# chant
			horret			# third keyword
		$", REGEX_OPTIONS);
	}

	public override bool ConditionsAreMet (IList<string> incantation)
	{
		return
			MirrorState.Instance.NumberBroken() >= 1 &&
			incantation[incantation.Count - 2] == Seance.TrueChant(getName(incantation));
	}

	public override IEnumerator CastBehavior (TerminalApp term, IList<string> incantation)
	{
		if (!MirrorState.Instance.TryConsumeMagic())
		{
			throw new Exception("unable to consume magic despite being told there were enough mirrors to consume");
		}

		// fun stuff starts here

		yield return new WaitForSeconds(.5f);

		term.PrintLine(randomAscii(UnityEngine.Random.Range(1, 3)));
		yield return null;
		term.PrintLine(randomAscii(UnityEngine.Random.Range(2, 4)));
		yield return null;

		yield return new WaitForSeconds(1);

		float timer = 1;
		int max = 4;

		while (timer > 0)
		{
			term.PrintLine(randomAscii(UnityEngine.Random.Range(1, max / 2)));
			max += 2;
			timer -= Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds(1f);

		// end fun stuff

		castAt(getName(incantation));

		yield return new WaitForSeconds(.3f);
	}

	string getName (IList<string> incantation)
	{
		StringBuilder name = new StringBuilder(incantation[1]);

		for (int i = 2; i < incantation.Count; i++)
		{
			if (incantation[i] == "fortunam") break;
			name.Append(" ");
			name.Append(incantation[i]);
		}

		return name.ToString().ToLower();
	}
}
