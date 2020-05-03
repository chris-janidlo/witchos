using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a casting is what is produced when a spell is cast at a target. it is the fact that a spell of a certain type was cast at a certain target
[Serializable]
public struct Casting
{
    public SpellType Type;
    public string TargetName;
    public string TargetTrueName => TrueName.FromName(TargetName);

    public static bool operator == (Casting a, Casting b)
    {
        return a.Type == b.Type && a.TargetName.Equals(b.TargetName, StringComparison.InvariantCultureIgnoreCase);
    }

    public static bool operator != (Casting a, Casting b)
    {
        return !(a == b);
    }

    public Casting (SpellType type, string targetTrueName)
    {
        Type = type;
        TargetName = targetTrueName;
    }

    public override bool Equals (object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;

        return this == (Casting) obj;
    }
    
    public override int GetHashCode ()
    {
        return new Tuple<SpellType, string>(Type, TargetName).GetHashCode();
    }

    public override string ToString ()
    {
        return Type.ToString() + " " + TargetName;
    }
}
