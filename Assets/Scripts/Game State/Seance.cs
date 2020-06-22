using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;
namespace WitchOS
{
public static class Seance
{
    public const int MAX_CHANTS = 7; // MUST be smaller than the amount of available chants

    public static readonly List<string> CHANTS = new List<string>
    {
        "homage to you",
        "you are seated on your throne",
        "the heart-soul has bourne destiny",
        "is there no sin in my body?",
        "he who protecteth you",
        "I shall not die again",
        "they shall fall and not be united again",
        "I have opened a path",
        "I hold this against you",
        "you have forsaken the love you had at first",
        "I know the blasphemy of them",
        "I know your deeds",
        "you have a reputation of being alive, but you are dead",
        "his feet as pillars of fire",
        "I saw a new heaven and a new earth",
        "curiosis fabricavit inferos",
        "lasciate ogni speranza, voi ch'entrate",
        "abandon all hope, ye who enter here",
        "but even Hell would not receive them",
        "here every cowardice must meet its death",
        "do you know not that we are worms",
        "worldly renown is naught but a breath of wind"
    };

    static readonly BagRandomizer<string> chantBag = new BagRandomizer<string> { Items = CHANTS, AvoidRepeats = true };

    public static IEnumerator<string> GetChants (string name)
    {
        string trueChant = TrueChant(name);

        int trueCount = 0;

        for (int i = 0; i < MAX_CHANTS; i++)
        {
            // guarantee that by the end of the chant we've seen at least 2 chants, and spread the guaranteed ones around a bit
            string next = (i >= MAX_CHANTS / 2 && trueCount < 1) || (i >= MAX_CHANTS - 1 && trueCount < 2)
                ? trueChant
                : chantBag.GetNext();

            if (next == trueChant)
            {
                trueCount++;
            }

            yield return next;
        }
    }

    public static string TrueChant (string name)
    {
        return CHANTS[Math.Abs(name.ToLowerInvariant().GetHashCode()) % CHANTS.Count];
    }
}
}
