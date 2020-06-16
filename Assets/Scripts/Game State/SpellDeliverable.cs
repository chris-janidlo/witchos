using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
[CreateAssetMenu(fileName = "NewSpellDeliverable.asset", menuName = "WitchOS/SpellDeliverable")]
public class SpellDeliverable : Deliverable<Spell>, IEquatable<SpellDeliverable>
{
    public string TargetName;

    public bool Equals (SpellDeliverable other)
    {
        // uses base reference equality of UnityEngine.Object
        return this == other;
    }

    public override string EmailAttachment ()
    {
        return $"{base.EmailAttachment()}\nTarget: {TargetName}";
    }
}
}
