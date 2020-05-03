using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class StubSpell : Spell
{
	public override SpellType Type => SpellType.Stub;

	public override Regex GetRegex ()
	{
		switch (TimeState.Instance.GetTodaysMoonPhase())
		{
			case MoonPhase.WaxingCrescent:
			case MoonPhase.WaningCrescent:
				return new Regex(@"^subgicatrix$", REGEX_OPTIONS);

			case MoonPhase.WaxingGibbous:
			case MoonPhase.WaningGibbous:
				return new Regex(@"^.+\s+subgicatrix", REGEX_OPTIONS);

			default:
				// every other phase is either `subgicatrix target_name` or `subgicatrix target_name_backward`, which are the same regex
				return new Regex(@"^subgicatrix\s+.+$", REGEX_OPTIONS);
		}
	}

	public override bool ConditionsAreMet (IList<string> incantation)
	{
		string aura = TerminalState.Instance.GetEnvironmentVariable("aura");

		switch (TimeState.Instance.GetTodaysMoonPhase())
		{
			case MoonPhase.WaxingGibbous:
			case MoonPhase.WaningGibbous:
				return aura == "nox";
			
			case MoonPhase.FullMoon:
				return aura == "lux";

			default:
				return true;
		}
	}

	public override IEnumerator CastBehavior (TerminalApp term, IList<string> incantation)
	{
		string target = TerminalState.Instance.GetEnvironmentVariable("target");

		string name;

		switch (TimeState.Instance.GetTodaysMoonPhase())
		{
			case MoonPhase.WaxingCrescent:
			case MoonPhase.WaningCrescent:
			case MoonPhase.WaxingGibbous:
			case MoonPhase.WaningGibbous:
				name = target;
				break;
			
			case MoonPhase.FullMoon:
				name = String.Join(" ", incantation.Skip(1));
				break;

			default:
				name = new String(string.Join(" ", incantation.Skip(1)).Reverse().ToArray());
				break;
		}

		yield return new WaitForSeconds(.5f);

		term.PrintEmptyLine();

		float timer = 2;

		while (timer > 0)
		{
			term.LastOutputLine += randomAscii(1);
			term.PaintOutputHistoryText();
			timer -= Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds(.5f);
		
		term.PrintEmptyLine();

		castAt(name);
	}
}
