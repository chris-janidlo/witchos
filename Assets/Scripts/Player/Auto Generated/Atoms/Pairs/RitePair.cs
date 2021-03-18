using System;
using UnityEngine;
using WitchOS;
namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// IPair of type `&lt;Rite&gt;`. Inherits from `IPair&lt;Rite&gt;`.
    /// </summary>
    [Serializable]
    public struct RitePair : IPair<Rite>
    {
        public Rite Item1 { get => _item1; set => _item1 = value; }
        public Rite Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private Rite _item1;
        [SerializeField]
        private Rite _item2;

        public void Deconstruct(out Rite item1, out Rite item2) { item1 = Item1; item2 = Item2; }
    }
}