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

			TerminalState.Instance.EnvironmentVariables[arguments[1]] = arguments[2];

			term.println(arguments[1] + " = " + arguments[2]);
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
					if (env.ContainsKey(key))
					{
						term.println(key + " = " + env[key]);
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
			string name = String.Join("", arguments.Skip(1));

			term.println("connecting to spirit world...");
			yield return new WaitForSeconds(3);
			
			term.println("connection succeeded.");
			term.println("now echoing the laments of the dead. press escape to stop.");

			yield return new WaitForSeconds(3);

			while (!term.SIGINT)
			{
				// TODO: get the proper chants to put down here
				term.println("oooooo plaaaaceeehoooooldeeer");

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

			if (arguments.Length != 2)
			{
				term.println("usage: xing ip|true_name");
				yield break;
			}

			IPAddress address;
			bool isIP = IPAddress.TryParse(arguments[1], out address);

			if (isIP || TrueName.IsTrueName(arguments[1]))
			{
				TerminalState.Instance.XingLock = true;

				term.println($"pointing imps toward {(isIP ? address.ToString() : arguments[1])} (this may take some time)...");
				yield return new WaitForSeconds(UnityEngine.Random.Range(10, 20));

				TerminalState.Instance.XingTarget = arguments[1];
				term.println("done. now entering stability mode.");
				term.println("");
				term.println("press escape at any time to exit and RELEASE the imps from their current target.");

				while (!term.SIGINT)
				{
					yield return null;
				}

				TerminalState.Instance.XingLock = false;
			}
			else
			{
				term.println("error - bad argument format. xing requires either an IP address for an electronic target or a truename for a human target (see command 'tn')");
				yield break;
			}
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
	};
}
