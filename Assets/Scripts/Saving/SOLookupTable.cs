using System;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
    public class SOLookupTable : Singleton<SOLookupTable>
    {
        public List<ScriptableObjectPathTuple> LookUpTable;

        void Awake ()
        {
            SingletonOverwriteInstance(this);
        }

        public int GetID (ScriptableObject so)
        {
            return LookUpTable.FindIndex(o => o.Asset == so);
        }

        public ScriptableObject GetSO (int id)
        {
            return LookUpTable[id].Asset;
        }
    }
}
