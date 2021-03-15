using System;
using UnityEngine;
using WitchOS;
namespace UnityAtoms
{
    /// <summary>
    /// IPair of type `&lt;SpellDeliverable&gt;`. Inherits from `IPair&lt;SpellDeliverable&gt;`.
    /// </summary>
    [Serializable]
    public struct SpellDeliverablePair : IPair<SpellDeliverable>
    {
        public SpellDeliverable Item1 { get => _item1; set => _item1 = value; }
        public SpellDeliverable Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private SpellDeliverable _item1;
        [SerializeField]
        private SpellDeliverable _item2;

        public void Deconstruct(out SpellDeliverable item1, out SpellDeliverable item2) { item1 = Item1; item2 = Item2; }
    }
}