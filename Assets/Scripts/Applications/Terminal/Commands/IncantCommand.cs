using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class IncantCommand : TerminalCommand
    {
        public List<Spell> Spells;

        [TextArea]
        public string MagicIsOff, SpellNotFound, SpellFailed, SpellSucceeded;

        public override IEnumerator Evaluate (TerminalApp term, string[] arguments)
        {
            if (arguments.Length < 2)
            {
                printUsage(term);
                yield break;
            }

            if (!MagicSource.Instance.On)
            {
                term.PrintLine(MagicIsOff);
                yield break;
            }

            var incantation = arguments.Skip(1).ToArray();
            var joinedIncantation = String.Join(" ", incantation);

            Spell spell = Spells.FirstOrDefault(s => s.GetRegex().IsMatch(joinedIncantation));

            if (spell == null)
            {
                term.PrintLine(SpellNotFound);
                yield break;
            }

            if (!spell.ConditionsAreMet(incantation))
            {
                term.PrintLine(SpellFailed);
                yield break;
            }

            yield return spell.CastBehavior(term, incantation);

            term.PrintLine(SpellSucceeded);
        }
    }
}
