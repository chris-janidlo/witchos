using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace WitchOS
{
    public class SocialMediaSpell : Spell
    {
        public BoolVariable XingLock;
        public StringVariable XingTarget;

        public override Regex GetRegex ()
        {
            return new Regex(@"^facies\s+libel.+$", REGEX_OPTIONS);
        }

        public override bool ConditionsAreMet (IList<string> incantation)
        {
            return
                MirrorState.Instance.NumberIntact() >= 1 &&
                XingLock.Value &&
                !String.IsNullOrEmpty(XingTarget.Value);
        }

        // TODO: make more forgiving in terms of URL
        public override IEnumerator CastBehavior (TerminalApp term, IList<string> incantation)
        {
            string targetLock =
                String.Join(" ", incantation.Skip(2)) +
                " on " +
                XingTarget.Value;

            term.PrintEmptyLine();

            yield return new WaitForSeconds(1);

            term.PrintLine(randomAscii(111, false));
            yield return new WaitForSeconds(.1f);

            term.PrintLine(randomAscii(222, false));
            yield return new WaitForSeconds(.2f);

            term.PrintLine(randomAscii(333, false));
            yield return new WaitForSeconds(.3f);

            term.PrintEmptyLine();

            yield return new WaitForSeconds(2);

            castAt(targetLock);
        }
    }
}
