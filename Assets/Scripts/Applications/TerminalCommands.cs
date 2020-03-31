using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// exclusively terminal commands go here
public partial class TerminalApp : MonoBehaviour
{
	class CHelp : Command
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

	static Dictionary<string, Command> Commands = new Dictionary<string, Command>
	{
		{ "help", new CHelp() }
	};
}
