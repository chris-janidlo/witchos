using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
// for (de)serialization
public class SOLookupTable : Singleton<SOLookupTable>
{
    public List<ScriptableObject> LookupTable;

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    public int GetID (ScriptableObject so)
    {
        return LookupTable.IndexOf(so);
    }

    public ScriptableObject GetSO (int id)
    {
        return LookupTable[id];
    }
}
}
