using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SocialMediaSpell : Spell
{
	public override SpellType Type => SpellType.SocialMedia;

	public override Regex GetRegex ()
	{
		return new Regex(@"^facies\s+libel.+$", REGEX_OPTIONS);
	}

	public override bool ConditionsAreMet (IList<string> incantation)
	{
		return
			MirrorState.Instance.NumberIntact() >= 1 &&
			TerminalState.Instance.XingLock &&
			!String.IsNullOrEmpty(TerminalState.Instance.XingTarget);
	}

	// TODO: make more forgiving in terms of URL
	public override IEnumerator CastBehavior (TerminalApp term, IList<string> incantation)
	{
		string targetLock =
			String.Join(" ", incantation.Skip(2)) +
			" on " +
			TerminalState.Instance.XingTarget;

		term.PrintEmptyLine();

		yield return new WaitForSeconds(1);

		term.PrintLine(randomAscii(111, false));
		yield return new WaitForSeconds(.1f);

		term.PrintLine(randomAscii(222, false));
		yield return new WaitForSeconds(.2f);

		term.PrintLine(randomAscii(333, false));
		yield return new WaitForSeconds(.3f);

		term.PrintEmptyLine();

		yield return new WaitForSeconds(2);

		castAt(targetLock);
	}
}
