using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncantCommand : TerminalCommand
{
	public string SpellNotFound, SpellFailed, SpellSucceeded;

	public override IEnumerator Evaluate (TerminalApp term, string[] arguments)
	{
		if (arguments.Length < 2)
		{
			printUsage(term);
			yield break;
		}

		var incantation = arguments.Skip(1).ToArray();

		bool spellFound = false, spellSucceeded = false;

		switch (arguments[1])
		{
			case "malam":
				spellFound = true;
				spellSucceeded = badLuck(term, incantation);
				break;
			case "piratica":
				spellFound = true;
				spellSucceeded = password(term, incantation);
				break;
			case "facies":
				spellFound = true;
				spellSucceeded = social(term, incantation);
				break;
			case "subgicatrix":
				spellFound = true;
				spellSucceeded = stub(term, incantation);
				break;
		}

		if (!spellFound && incantation.Contains("borealeo"))
		{
			spellFound = true;
			spellSucceeded = hair(term, incantation);
		}

		if (!spellFound)
		{
			term.PrintLine(SpellNotFound);
			yield break;
		}

		yield return new WaitForSeconds(.3f);

		term.PrintLine(spellSucceeded ? SpellSucceeded : SpellFailed);
	}

	bool stub (TerminalApp term, string[] incantation)
	{
		string target = TerminalState.Instance.GetEnvironmentVariable("target");
		string aura = TerminalState.Instance.GetEnvironmentVariable("aura");

		switch (TimeState.Instance.GetTodaysMoonPhase())
		{
			case MoonPhase.WaxingCrescent:
			case MoonPhase.WaningCrescent:
				if (incantation.Length != 1 || target == "")
					return false;
				
				SpellWatcher.Instance.CastSpell
				(
					SpellType.Stub,
					target
				);
				
				return true;
			
			case MoonPhase.WaxingGibbous:
			case MoonPhase.WaningGibbous:
				if (incantation.Length != 1 || target == "" || aura != "nox")
					return false;
				
				SpellWatcher.Instance.CastSpell
				(
					SpellType.Stub,
					target
				);

				return true;

			case MoonPhase.FullMoon:
				if (incantation.Length == 1 || aura != "lux")
					return false;
				
				SpellWatcher.Instance.CastSpell
				(
					SpellType.Stub,
					string.Join(" ", incantation.Skip(1))
				);

				return true;
			
			default:
				if (incantation.Length == 1)
					return false;
				
				SpellWatcher.Instance.CastSpell
				(
					SpellType.Stub,
					new String(string.Join(" ", incantation.Skip(1)).Reverse().ToArray())
				);

				return true;
		}
	}

	bool badLuck (TerminalApp term, string[] incantation)
	{
		if (incantation.Length < 5 || TerminalState.Instance.GetEnvironmentVariable("aura") != "negative") return false;

		int mirrorCount = TimeState.Instance.GetTodaysMoonPhase().GetPhaseChange() == MoonPhaseChange.Waxing ? 2 : 1;
		if (MirrorState.Instance.NumberBroken() < mirrorCount) return false;

		List<string> nameBits = new List<string>();
		int i;
		for (i = 1; i < incantation.Length; i++)
		{
			if (incantation[i] == "fortunam") break;
			nameBits.Add(incantation[i]);
		}

		if (incantation.Length - i < 2) return false;

		string name = String.Join(" ", nameBits);

		if (incantation[i + 1] != Seance.TrueChant(name) || incantation[i + 2] != "horret") return false;

		for (i = 0; i < mirrorCount; i++)
		{
			if (!MirrorState.Instance.TryConsumeMagic()) throw new Exception("wtf");
		}

		SpellWatcher.Instance.CastSpell
		(
			SpellType.BadLuck,
			name
		);

		return true;
	}

	bool password (TerminalApp term, string[] incantation)
	{
		if (String.Join(" ", incantation) != "piratica non grata") return false;
		if (!TerminalState.Instance.XingLock) return false;

		string target = TerminalState.Instance.GetEnvironmentVariable("target");
		if (target == "" || target != TerminalState.Instance.XingTarget) return false;

		SpellWatcher.Instance.CastSpell
		(
			SpellType.Password,
			target
		);

		return true;
	}

	bool social (TerminalApp term, string[] incantation)
	{
		if (incantation[1] != "libel") return false;
		if (!TerminalState.Instance.XingLock) return false;
		if (MirrorState.Instance.NumberIntact() == 0) return false;

		string name = String.Join(" ", incantation.Skip(2));
		string website = TerminalState.Instance.XingTarget;

		SpellWatcher.Instance.CastSpell
		(
			SpellType.SocialMedia,
			name + " on " + website
		);

		return true;
	}

	bool hair (TerminalApp term, string[] incantation)
	{
		if (incantation[incantation.Length - 1] != "borealeo") return false;

		if (TerminalState.Instance.GetEnvironmentVariable("aura") != "null") return false;

		int mirrorCount = TimeState.Instance.GetTodaysMoonPhase().GetPhaseChange() == MoonPhaseChange.Waning ? 2 : 1;
		if (MirrorState.Instance.NumberBroken() < mirrorCount) return false;

		string name = incantation[0];
		for (int i = 1; i < incantation.Length - 1; i++)
		{
			name += " " + incantation[i];
		}

		SpellWatcher.Instance.CastSpell
		(
			SpellType.HairLoss,
			name
		);

		for (int i = 0; i < mirrorCount; i++)
		{
			if (!MirrorState.Instance.TryConsumeMagic()) throw new Exception("wtf");
		}

		return true;
	}
}
