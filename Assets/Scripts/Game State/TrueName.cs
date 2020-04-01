using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrueName
{
    public static string[] Syllables =
    {
        "be", "bo", "bi", "bu",
        "de", "do", "di",
        "fe", "fo", "fi", "fu",
        "ge", "go", "gi",
        "he",
              "jo", "ji",
        "ke", "ko", "ki", "ku",
        "le", "lo", "li", "lu",
        "me", "mo", "mi", "mu",
        "ne", "no", "ni", "nu",
        "pe", "po", "pi",
        "re", "ro", "ri", "ru",
        "se", "so", "si", "su",
        "te", "to", "ti", "tu",
        "ve", "vo", "vi", "vu",
        "we", "wo", "wi", "wu",
        "ye", "yo", "yi", "yu",
        "ze", "zo", "zi", "zu",
    };

    public static string FromName (string name)
    {
        UInt32 unsignedHash = (UInt32) name.ToLower().GetHashCode();

        string result = "";

        // in one step, convert the unsigned hash to base syllables.length, reverse it, and map the digits of the new representation to syllables
        while (unsignedHash != 0)
        {
            result += Syllables[unsignedHash % Syllables.Length];
            unsignedHash /= (uint) Syllables.Length;
        }

        return result;
    }

    public static bool IsTrueName (string name)
    {
        if (name.Split().Length != 1) return false;

        return chunk(name).All(c => Syllables.Contains(c));
    }

    static IEnumerable<string> chunk (string str)
    {
        for (int i = 0; i < str.Length; i += 2)
        {
            yield return str.Substring(i, Math.Min(2, str.Length-i));
        }
    }
}
