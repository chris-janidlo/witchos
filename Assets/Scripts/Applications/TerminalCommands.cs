using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// exclusively terminal commands go here
public partial class TerminalApp : MonoBehaviour
{
	class Chelp : Command
	{
		public IEnumerator Evaluate (string[] arguments)
		{
			yield return new WaitForSeconds(1);

			println("this is a test");

			foreach (string arg in arguments)
			{
				yield return new WaitForSeconds(.1f);
				println(arg);
			}
		}
	}

	class Ctn : Command
	{
		public IEnumerator Evaluate (string[] arguments)
		{
			if (arguments.Length == 1)
			{
				println("usage: tn name");
				yield break;
			}

			yield return new WaitForSeconds(.5f); // pretend to process something

			string trueName = TrueName.FromName(String.Join(" ", arguments.Skip(1)));
			println(trueName);
		}
	}

	class Cset : Command
	{
		public IEnumerator Evaluate (string[] arguments)
		{
			// TODO: allow for other argument styles like 'set a = b', 'set a=b', etc
			if (arguments.Length < 3)
			{
				println("usage: set name value");
				yield break;
			}

			TerminalState.Instance.EnvironmentVariables[arguments[1]] = arguments[2];

			println(arguments[1] + " = " + arguments[2]);
		}
	}

	class Cshow : Command
	{
		public IEnumerator Evaluate (string[] arguments)
		{
			var env = TerminalState.Instance.EnvironmentVariables;
			if (arguments.Length == 1)
			{
				foreach (var pair in env)
				{
					println(pair.Key + " = " + pair.Value);
					yield return null;
				}
			}
			else
			{
				foreach (var key in arguments.Skip(1))
				{
					if (env.ContainsKey(key))
					{
						println(key + " = " + env[key]);
					}
					else
					{
						println(key + " not set");
					}
					yield return null;
				}
			}
		}
	}

	static Dictionary<string, Command> Commands = new Dictionary<string, Command>
	{
		{ "help", new Chelp() },
		{ "tn", new Ctn() },
		{ "set", new Cset() },
		{ "show", new Cshow() }
	};
}
