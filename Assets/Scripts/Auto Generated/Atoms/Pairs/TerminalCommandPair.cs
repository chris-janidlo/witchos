using System;
using UnityEngine;
using WitchOS;
namespace UnityAtoms
{
    /// <summary>
    /// IPair of type `&lt;TerminalCommand&gt;`. Inherits from `IPair&lt;TerminalCommand&gt;`.
    /// </summary>
    [Serializable]
    public struct TerminalCommandPair : IPair<TerminalCommand>
    {
        public TerminalCommand Item1 { get => _item1; set => _item1 = value; }
        public TerminalCommand Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private TerminalCommand _item1;
        [SerializeField]
        private TerminalCommand _item2;

        public void Deconstruct(out TerminalCommand item1, out TerminalCommand item2) { item1 = Item1; item2 = Item2; }
    }
}