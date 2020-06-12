using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
public class SpellWatcher : Singleton<SpellWatcher>
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
