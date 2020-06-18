using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityAtoms;
using crass;

namespace WitchOS
{
public abstract class Spell : Service
{
    public SpellDeliverableValueList SpellEther;

    protected const RegexOptions REGEX_OPTIONS = RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase;

    // determine the regular expression that incant uses to disambiguate spells
    // should generally only do whole string matches, ie ^<some-regex>$
    // unless there's a good reason otherwise, all regexes should be instantiated with REGEX_OPTIONS as the second argument to the constructor
    public abstract Regex GetRegex ();

    // check for other spell conditions beside incantation, such as mirrors or moon phase
    // incantation is the entire argument list to `incant`, excluding the word `incant`
    public abstract bool ConditionsAreMet (IList<string> incantation);

    // anything that should happen when this spell is cast
    public abstract IEnumerator CastBehavior (TerminalApp term, IList<string> incantation);

    protected void castAt (string target)
    {
        SpellDeliverable spellDeliverable = ScriptableObject.CreateInstance(typeof(SpellDeliverable)) as SpellDeliverable;
        spellDeliverable.name = $"in-memory cast of {PrettyName} at {target}";
        spellDeliverable.Service = this;
        spellDeliverable.TargetName = target;

        SpellEther.Add(spellDeliverable);
    }

    // for spooky effects
    protected string randomAscii (int length, bool includeSpace = true)
    {
        Vector2Int printableCharacterRange = new Vector2Int
        (
            32 + (includeSpace ? 0 : 1),
            127 // don't include delete (random is exclusive)
        );

        StringBuilder sb = new StringBuilder();

        bool colorAdded = false;

        for (int i = 0; i < length; i++)
        {
            if (!colorAdded && RandomExtra.Chance(.02f))
            {
                // green to purple (no red or yellow), upper half of saturation, only max lightness
                var color = Random.ColorHSV(.25f, .85f, .5f, 1, 1, 1);

                sb.Append($"<#{ColorUtility.ToHtmlStringRGB(color)}>");
                colorAdded = true;
            }

            sb.Append((char) RandomExtra.Range(printableCharacterRange));

            if (colorAdded && RandomExtra.Chance(.07f))
            {
                sb.Append("</color>");
                colorAdded = false;
            }
        }

        if (colorAdded)
        {
            sb.Append("</color>");
            colorAdded = false;
        }

        return sb.ToString();
    }
}
}
