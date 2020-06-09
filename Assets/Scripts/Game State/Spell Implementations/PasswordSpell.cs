using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using crass;

namespace WitchOS
{
public class PasswordSpell : Spell
{
	public override SpellType Type => SpellType.Password;

	string envTarget => TerminalState.Instance.GetEnvironmentVariable("target");

	static List<int> progChanges = new List<int>() {-1, 2, 3, 4, 5};

	public override Regex GetRegex ()
	{
		return new Regex(@"^signus\s+salis$", REGEX_OPTIONS);
	}

	public override bool ConditionsAreMet (IList<string> incantation)
	{
		return TerminalState.Instance.XingLock && envTarget == TerminalState.Instance.XingTarget;
	}

	public override IEnumerator CastBehavior (TerminalApp term, IList<string> incantation)
	{
		string targetLock = envTarget;

		float timer = 5;
		int progress = 0;

		term.PrintEmptyLine();

		while (timer > 0)
		{
			term.LastOutputLine = $"progress: [{new String('=', progress).PadRight(10)}]";
			term.PaintOutputHistoryText();

			progress = Mathf.Max(0, progress + progChanges.PickRandom());

			float delta = UnityEngine.Random.Range(.5f, 2);
			yield return new WaitForSeconds(delta);
			timer -= delta;
		}

		term.LastOutputLine = $"progress: [{new String(' ', 10)}]";

		term.PrintEmptyLine();

		castAt(targetLock);

		yield return new WaitForSeconds(.3f);
	}
}
}
