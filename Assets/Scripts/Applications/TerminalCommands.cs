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
		public IEnumerator Evaluate (TerminalApp term, string[] arguments)
		{
			yield return new WaitForSeconds(1);

			term.println("this is a test");

			foreach (string arg in arguments)
			{
				yield return new WaitForSeconds(.1f);
				term.println(arg);
			}
		}
	}

	class Ctn : Command
	{
		public IEnumerator Evaluate (TerminalApp term, string[] arguments)
		{
			if (arguments.Length == 1)
			{
				term.println("usage: tn name");
				yield break;
			}

			yield return new WaitForSeconds(.5f); // pretend to process something

			string trueName = TrueName.FromName(String.Join(" ", arguments.Skip(1)));
			term.println(trueName);
		}
	}

	class Cset : Command
	{
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
		public IEnumerator Evaluate (TerminalApp term, string[] arguments)
		{
			string name = String.Join(" ", arguments.Skip(1));

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
