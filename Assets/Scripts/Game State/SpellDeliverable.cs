using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
[Serializable]
public class SpellDeliverable : Deliverable<Spell>
{
    public string TargetName;

    public static bool operator == (SpellDeliverable a, SpellDeliverable b)
    {
        return a.Service == b.Service && a.TargetName.Equals(b.TargetName, StringComparison.InvariantCultureIgnoreCase);
    }

    public static bool operator != (SpellDeliverable a, SpellDeliverable b)
    {
        return !(a == b);
    }

    public override bool Equals (object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;

        return this == (SpellDeliverable) obj;
    }

    public override int GetHashCode ()
    {
        return new Tuple<Spell, string>(Service, TargetName).GetHashCode();
    }
}
}
