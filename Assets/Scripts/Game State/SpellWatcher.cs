using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
public class SpellWatcher : Singleton<SpellWatcher>
{
    public event Action<Casting> SpellCast;

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    public void CastSpell (Casting casting)
    {
        SpellCast?.Invoke(casting);
    }

    public void CastSpell (SpellType type, string targetTrueName)
    {
        CastSpell(new Casting(type, targetTrueName));
    }
}
}
