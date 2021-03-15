using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace WitchOS
{
    public class StubSpell : Spell
    {
        public override Regex GetRegex ()
        {
            switch (TimeState.Instance.GetTodaysMoonPhase())
            {
                case MoonPhase.WaxingGibbous:
                case MoonPhase.WaningGibbous:
                case MoonPhase.FullMoon:
                    return new Regex(@"^subgicatrix\s+.+$", REGEX_OPTIONS);

                case MoonPhase.WaxingCrescent:
                case MoonPhase.WaningCrescent:
                    return new Regex(@"^subgicatrix$", REGEX_OPTIONS);

                default:
                    return new Regex(@"^.+\s+subgicatrix", REGEX_OPTIONS);
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

                case MoonPhase.WaxingCrescent:
                case MoonPhase.WaningCrescent:
                    return TerminalState.Instance.EnvironmentVariables.ContainsKey("target");

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
                case MoonPhase.WaxingGibbous:
                case MoonPhase.WaningGibbous:
                    name = String.Join(" ", incantation.Skip(1));
                    break;

                case MoonPhase.WaxingCrescent:
                case MoonPhase.WaningCrescent:
                    name = target;
                    break;

                case MoonPhase.FullMoon:
                    name = new String(string.Join(" ", incantation.Skip(1)).Reverse().ToArray());
                    break;

                default:
                    name = string.Join(" ", incantation.Take(incantation.Count - 1));
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
}
