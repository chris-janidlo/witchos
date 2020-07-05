using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace WitchOS
{
    public class XingCommand : TerminalCommand
    {
        public BoolVariable XingLock;
        public StringVariable XingTarget;

        public override IEnumerator Evaluate (TerminalApp term, string[] arguments)
        {
            if (XingLock.Value)
            {
                term.PrintLine("error - xing is already running in another window");
                yield break;
            }

            if (arguments.Length < 2)
            {
                printUsage(term);
                yield break;
            }

            if (!MagicSource.Instance.On)
            {
                term.PrintLine("error - unable to contact the imps unless magic is on");
                yield break;
            }

            string target = String.Join(" ", arguments.Skip(1));

            XingLock.Value = true;

            term.PrintLine($"pointing imps toward {target}... (press ESC to cancel if desired. this may take some time)");

            float timer = UnityEngine.Random.Range(10, 20);
            while (!term.SIGINT && timer >= 0)
            {
                yield return null;
                timer -= Time.deltaTime;
            }

            if (!term.SIGINT)
            {
                XingTarget.Value = target;
                term.PrintEmptyLine();
                term.PrintLine("done.");
                yield return new WaitForSeconds(.3f);
                term.PrintLine("now entering stability mode.");
                yield return new WaitForSeconds(.5f);
                term.PrintEmptyLine();
                term.PrintLine("press escape at any time to exit and release the imps from their current target.");
            }

            while (!term.SIGINT)
            {
                yield return null;
            }

            XingLock.Value = false;
        }

        public override void CleanUpEarly (TerminalApp term)
        {
            XingLock.Value = false;
        }
    }
}
