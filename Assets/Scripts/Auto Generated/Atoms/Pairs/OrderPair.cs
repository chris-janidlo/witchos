using System;
using UnityEngine;
using WitchOS;
namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// IPair of type `&lt;Order&gt;`. Inherits from `IPair&lt;Order&gt;`.
    /// </summary>
    [Serializable]
    public struct OrderPair : IPair<Order>
    {
        public Order Item1 { get => _item1; set => _item1 = value; }
        public Order Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private Order _item1;
        [SerializeField]
        private Order _item2;

        public void Deconstruct(out Order item1, out Order item2) { item1 = Item1; item2 = Item2; }
    }
}