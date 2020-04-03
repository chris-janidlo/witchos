using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class SpellWatcher : Singleton<SpellWatcher>
{
    public event Action<Spell> SpellCast;

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    public void CastSpell (Spell spell)
    {
        SpellCast?.Invoke(spell);
    }

    public void CastSpell (SpellType type, string targetTrueName)
    {
        CastSpell(new Spell(type, targetTrueName));
    }
}
