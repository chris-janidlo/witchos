using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class SOInitializer : MonoBehaviour
    {
        public List<InitializableSO> ScriptableObjects;

        void Awake ()
        {
            ScriptableObjects.ForEach(s => s.Initialize());
        }
    }
}
