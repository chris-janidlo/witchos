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

    public Spell (SpellType type, string targetTrueName)
    {
        Type = type;
        TargetName = targetTrueName;
    }
}

public enum SpellType
{
    BadLuck, Stub, HairLoss, Password, SocialMedia
}
