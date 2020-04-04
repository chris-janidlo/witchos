using System;
using System.Net;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// exclusively terminal commands go here
public partial class TerminalApp : MonoBehaviour
{
	class Chelp : Command
	{
		public string[] HelpOutput => new string[]
		{
			"displays help information for commands"
		};

		public IEnumerator Evaluate (TerminalApp term, string[] arguments)
		{
			if (arguments.Length == 1)
			{
				term.println("available commands:");
				term.println(" " + String.Join(" ", Commands.Keys.OrderBy(c => c)));
				term.println("enter 'help' followed by a command name to learn more");
			}
			else
			{
				string com = arguments[1];
				if (Commands.ContainsKey(com))
				{
					foreach (var line in Commands[com].HelpOutput)
					{
						term.println(line);
					}
				}
				else
				{
					term.println($"help: can't find any command named '{com}'");
				}
			}

			yield return null;
		}
	}

	class Ctn : Command
	{
		public string[] HelpOutput => new string[]
		{
			"for discovering the True Name of an individual",
			"input can be given as either 'tn firstname lastname' or 'tn fullname'. for example, 'tn john smith' or 'tn johnsmith'. capitalization is unimportant."
		};

		public IEnumerator Evaluate (TerminalApp term, string[] arguments)
		{
			if (arguments.Length == 1)
			{
				term.println("usage: tn name");
				yield break;
			}

			yield return new WaitForSeconds(.5f); // pretend to process something

			string trueName = TrueName.FromName(String.Join("", arguments.Skip(1)));
			term.println(trueName);
		}
	}

	class Cset : Command
	{
		public string[] HelpOutput => new string[]
		{
			"lets you set environment variables",
			"environment variables are just a way of storing values by name so other commands/programs can access them",
			"for example, if you enter 'set aura null', any other command that wants to know what the current value of 'aura' is would see that it is 'null'"
		};

		public IEnumerator Evaluate (TerminalApp term, string[] arguments)
		{
			// TODO: allow for other argument styles like 'set a = b', 'set a=b', etc
			if (arguments.Length < 3)
			{
				term.println("usage: set name value");
				yield break;
			}

			string value = String.Join(" ", arguments.Skip(2));

			TerminalState.Instance.EnvironmentVariables[arguments[1]] = String.Join(" ", value);
			term.println(arguments[1] + " = " + value);
		}
	}

	class Cshow : Command
	{
		public string[] HelpOutput => new string[]
		{
			"displays values of environment variables",
			"if this isn't given any arguments, it simply displays every environment variable that currently has a value",
			"otherwise, the command tries to display the current value for every argument",
			"(see 'help set' for more information on environment variables)"
		};

		public IEnumerator Evaluate (TerminalApp term, string[] arguments)
		{
			var env = TerminalState.Instance.EnvironmentVariables;
			if (arguments.Length == 1)
			{
				foreach (var pair in env)
				{
					term.println(pair.Key + " = " + pair.Value);
					yield return null;
				}
			}
			else
			{
				foreach (var key in arguments.Skip(1))
				{
					string result;
					if (!env.TryGetValue(key, out result))
					{
						term.println(key + " = " + result);
					}
					else
					{
						term.println(key + " not set");
					}
					yield return null;
				}
			}
		}
	}

	class Cseance : Command
	{
		public string[] HelpOutput => new string[]
		{
			"for communing with the spirits of the dead",
			"given a person's name, seance tunes in to the spirit world and finds that persons ancestors, so that the user can read their chanting"
		};

		public IEnumerator Evaluate (TerminalApp term, string[] arguments)
		{
			if (arguments.Length == 1)
			{
				term.println("usage: seance name");
				yield break;
			}

			string name = String.Join(" ", arguments.Skip(1));

			term.println("connecting to spirit world...");
			yield return new WaitForSeconds(3);
			
			term.println("connection succeeded.");
			term.println("now echoing the laments of the dead. press escape to stop.");

			var laments = Seance.GetChants(name);

			yield return new WaitForSeconds(3);

			while (!term.SIGINT)
			{
				term.println(laments.Current);
				laments.MoveNext();

				// roll our own weird WaitForSeconds so that we can immediately break if any key is indeed pressed
				float timer = UnityEngine.Random.Range(.5f, 3);
				while (!term.SIGINT && timer >= 0)
				{
					yield return null;
					timer -= Time.deltaTime;
				}
			}
		}
	}

	class Cxing : Command
	{
		public string[] HelpOutput => new string[]
		{
			"aligns mischievous imps toward a target",
			"xing is useful for spells that utilize imps for hacking devices or human brains",
			"it takes either an IP address (for devices) or a person's true name as an argument",
			"while active, any spell that needs imps can effect xing's current target."
		};

		public IEnumerator Evaluate (TerminalApp term, string[] arguments)
		{
			if (TerminalState.Instance.XingLock)
			{
				term.println("error - xing is already running in another window");
				yield break;
			}

			if (arguments.Length < 2)
			{
				term.println("usage: xing website|name");
				yield break;
			}

			string target = String.Join(" ", arguments.Skip(1));

			TerminalState.Instance.XingLock = true;

			term.println($"pointing imps toward {target}... (press ESC to cancel if desired. this may take some time)");

			float timer = UnityEngine.Random.Range(10, 20);
			while (!term.SIGINT && timer >= 0)
			{
				yield return null;
				timer -= Time.deltaTime;
			}

			TerminalState.Instance.XingTarget = target;
			term.println("done. now entering stability mode.");
			term.println("");
			term.println("press escape at any time to exit and RELEASE the imps from their current target.");

			while (!term.SIGINT)
			{
				yield return null;
			}

			TerminalState.Instance.XingLock = false;
		}
	}

	class Cincant : Command
	{
		public string[] HelpOutput => new string[]
		{
			"invoke the ancient magicks and cast a spell or curse"
		};

		public IEnumerator Evaluate (TerminalApp term, string[] arguments)
		{
			if (arguments.Length < 2)
			{
				term.println("usage: incant spell");
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
				term.println("unable to hook into the weave - double check the spelling of your incantation");
				yield break;
			}

			yield return new WaitForSeconds(.3f);

			term.println
			(
				spellSucceeded
					? "spell successfully cast"
					: "the arcane aether fizzled out - something was missing in your spell"
			);
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
			if (incantation.Length != 5 || TerminalState.Instance.GetEnvironmentVariable("aura") != "negative") return false;

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

			string name = String.Join(" ", incantation.Skip(1));
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

	static Dictionary<string, Command> Commands = new Dictionary<string, Command>
	{
		{ "help", new Chelp() },
		{ "tn", new Ctn() },
		{ "set", new Cset() },
		{ "show", new Cshow() },
		{ "seance", new Cseance() },
		{ "xing", new Cxing() },
		{ "incant", new Cincant() },
	};
}
