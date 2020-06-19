using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;
namespace WitchOS
{
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
            "I have opened a path",
            "I hold this against you",
            "you have forsaken the love you had at first",
            "I know the blasphemy of them",
            "I know your deeds",
            "you have a reputation of being alive, but you are dead",
            "his feet as pillars of fire",
            "I saw a new heaven and a new earth",
            "the first heaven and the first earth were passed away"
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
                otherBullshitCounter++;
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
}
