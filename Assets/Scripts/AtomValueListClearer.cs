using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

public class AtomValueListClearer : MonoBehaviour
{
    public List<BaseAtomValueList> AtomValueLists;

    void OnEnable ()
    {
        foreach (var list in AtomValueLists)
        {
            list.Clear();
        }
    }
}
