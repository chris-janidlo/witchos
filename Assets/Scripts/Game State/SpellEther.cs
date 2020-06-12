using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
// spells are cast cast into the spell ether. once cast, they exist in the ether for the rest of the day
// TODO: (for future tickets) make this an actual container
public class SpellEther : Singleton<SpellEther>
{
    public event Action<SpellDeliverable> SpellCast;

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    public void CastSpell (SpellDeliverable spellDeliverable)
    {
        SpellCast?.Invoke(spellDeliverable);
    }

    public void CastSpell (Spell spell, string targetName)
    {
        CastSpell(new SpellDeliverable { Service = spell, TargetName = targetName });
    }
}
}
