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
            return base.Equals(other) && TargetName.Equals(other.TargetName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override string EmailAttachment ()
        {
            return $"{base.EmailAttachment()}\nTarget: {TargetName}";
        }
    }
}
