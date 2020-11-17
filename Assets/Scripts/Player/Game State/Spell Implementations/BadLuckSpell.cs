using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using crass;

namespace WitchOS
{
    public class BadLuckSpell : Spell
    {
        public override Regex GetRegex ()
        {
            return new Regex
            (@"^
			    malam\s+		# first keyword, followed by space characters
			    fortunam\s+		# second keyword, followed by space characters
			    .*\.\s+			# name, followed by a period and one or more space characters
			    .*				# chant
		    $", REGEX_OPTIONS);
        }

        public override bool ConditionsAreMet (IList<string> incantation)
        {
            string ourChant = getChant(incantation), theirChant = Seance.TrueChant(getName(incantation));

            return
                NumBrokenMirrors.Value >= 1 &&
                ourChant.Equals(theirChant, StringComparison.InvariantCultureIgnoreCase);
            // getChant(incantation).Equals(Seance.TrueChant(getName(incantation)), StringComparison.InvariantCultureIgnoreCase);
        }

        public override IEnumerator CastBehavior (ITerminal term, IList<string> incantation)
        {
            TryConsumeMirrorMagic.Raise();

            // fun stuff starts here

            yield return new WaitForSeconds(.5f);

            term.PrintSingleLine(randomAscii(UnityEngine.Random.Range(1, 3)));
            yield return null;
            term.PrintSingleLine(randomAscii(UnityEngine.Random.Range(2, 4)));
            yield return null;

            yield return new WaitForSeconds(1);

            float timer = 1;
            int max = 4;

            while (timer > 0)
            {
                term.PrintSingleLine(randomAscii(UnityEngine.Random.Range(1, max / 2)));
                max += 2;
                timer -= Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            // end fun stuff

            castAt(getName(incantation));

            yield return new WaitForSeconds(.3f);
        }

        string getName (IList<string> incantation)
        {
            List<string> nameBits = new List<string>();

            foreach (string word in incantation.Skip(2))
            {
                if (word.EndsWith("."))
                {
                    nameBits.Add(word.Substring(0, word.Length - 1));
                    break;
                }
                else
                {
                    nameBits.Add(word);
                }
            }

            return String.Join(" ", nameBits);
        }

        string getChant (IList<string> incantation)
        {
            return String.Join(" ", incantation.SkipWhile(w => !w.EndsWith(".")).Skip(1));
        }
    }
}
