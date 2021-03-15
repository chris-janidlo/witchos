using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    [Serializable, DataContract]
    public class SpellDeliverable : Deliverable<Spell>, IEquatable<SpellDeliverable>
    {
        [Serializable, DataContract]
        public class SaveableSpellReference : SaveableScriptableObjectReference<Spell> { }

        [SerializeField]
        [DataMember(IsRequired = true)]
        private SaveableSpellReference _spellReference;

        [DataMember(IsRequired = true)]
        public string TargetName;

        public override Spell Service => _spellReference.Value;

        public SpellDeliverable (Spell service, string targetName)
        {
            _spellReference = new SaveableSpellReference { Value = service };
            TargetName = targetName;
        }

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
