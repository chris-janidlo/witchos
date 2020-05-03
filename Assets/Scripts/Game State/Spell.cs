using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using crass;

public abstract class Spell
{
    protected const RegexOptions REGEX_OPTIONS = RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase;

    public abstract SpellType Type { get; }

    // determine the regular expression that incant uses to disambiguate spells
    // should generally only do whole string matches, ie ^<some-regex>$
    // unless there's a good reason otherwise, all regexes should be instantiated with REGEX_OPTIONS as the second argument to the constructor
    public abstract Regex GetRegex ();

    // check for other spell conditions beside incantation, such as mirrors or moon phase
    // incantation is the entire argument list to `incant`, excluding the word `incant`
    public abstract bool ConditionsAreMet (IList<string> incantation);

    // anything that should happen when this spell is cast
    public abstract IEnumerator CastBehavior (TerminalApp term, IList<string> incantation);

    // for DRYness
    protected void castAt (string target)
    {
        SpellWatcher.Instance.CastSpell(Type, target);
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

        for (int i = 0; i < length; i++)
        {
            sb.Append((char) RandomExtra.Range(printableCharacterRange));
        }

        return sb.ToString();
    }
}
