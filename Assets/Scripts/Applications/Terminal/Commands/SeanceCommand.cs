using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class SeanceCommand : TerminalCommand
    {
        public override IEnumerator Evaluate (ITerminal term, string[] arguments)
        {
            if (arguments.Length == 1)
            {
                printUsage(term);
                yield break;
            }

            if (!MagicSource.Instance.On)
            {
                term.PrintSingleLine("cannot connect to the spirit world while magic is off");
                yield break;
            }

            string name = String.Join(" ", arguments.Skip(1));

            term.PrintSingleLine("connecting to spirit world...");
            yield return new WaitForSeconds(3);

            term.PrintSingleLine("connection succeeded.");
            term.PrintEmptyLine();
            term.PrintSingleLine("now echoing the laments of the dead. press escape to stop.");
            term.PrintEmptyLine();

            var laments = Seance.GetChants(name);

            yield return new WaitForSeconds(3);

            while (!term.WasInterrupted && laments.MoveNext())
            {
                term.PrintSingleLine(laments.Current);

                // roll our own weird WaitForSeconds so that we can immediately break if escape key is pressed
                float timer = UnityEngine.Random.Range(.5f, 3);
                while (!term.WasInterrupted && timer >= 0)
                {
                    yield return null;
                    timer -= Time.deltaTime;
                }
            }

            term.PrintEmptyLine();
        }
    }
}
