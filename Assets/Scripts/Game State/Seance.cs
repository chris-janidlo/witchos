using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public static class Seance
{
    const float otherBullshitChance = 0.6f;
    const float otherBullshitMax = 3;

    static BagRandomizer<string> otherBullshit = new BagRandomizer<string>
    {
        Items =  new List<string>
        {
            "homage to you",
            "you are seated on your throne",
            "the heart-soul has bourne destiny on its behalf",
            "is there no sin in my body?",
            "he who protecteth you for millions of years",
            "I shall not die again",
            "they shall fall and not be united again",
            "I have opened a path"
        },
        AvoidRepeats = true
    };

    public static IEnumerator<string> GetChants (string trueName)
    {
        int otherBullshitCounter = 0;

        while (true)
        {
            if (otherBullshitCounter < otherBullshitMax && RandomExtra.Chance(otherBullshitChance))
            {
                yield return otherBullshit.GetNext();
            }
            else
            {
                yield return TrueChant(trueName);
            }
        }
    }

    public static string TrueChant (string trueName)
    {
        return TrueName.FromName(TrueName.FromName(trueName)).Substring(0, 4);
    }
}
