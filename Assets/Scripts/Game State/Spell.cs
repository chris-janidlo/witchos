using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Spell
{
    public SpellType Type;
    public string TargetName;
    public string TargetTrueName => TrueName.FromName(TargetName);

    public static bool operator == (Spell a, Spell b)
    {
        return a.Type == b.Type && a.TargetName.Equals(b.TargetName, StringComparison.InvariantCultureIgnoreCase);
    }

    public static bool operator != (Spell a, Spell b)
    {
        return !(a == b);
    }

    public Spell (SpellType type, string targetTrueName)
    {
        Type = type;
        TargetName = targetTrueName;
    }

    public override bool Equals (object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;

        return this == (Spell) obj;
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

public enum SpellType
{
    BadLuck, Stub, HairLoss, Password, SocialMedia
}
