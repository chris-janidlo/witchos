using System;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Reference of type `TerminalCommand`. Inherits from `EquatableAtomReference&lt;TerminalCommand, TerminalCommandPair, TerminalCommandConstant, TerminalCommandVariable, TerminalCommandEvent, TerminalCommandPairEvent, TerminalCommandTerminalCommandFunction, TerminalCommandVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class TerminalCommandReference : EquatableAtomReference<
        TerminalCommand,
        TerminalCommandPair,
        TerminalCommandConstant,
        TerminalCommandVariable,
        TerminalCommandEvent,
        TerminalCommandPairEvent,
        TerminalCommandTerminalCommandFunction,
        TerminalCommandVariableInstancer>, IEquatable<TerminalCommandReference>
    {
        public TerminalCommandReference() : base() { }
        public TerminalCommandReference(TerminalCommand value) : base(value) { }
        public bool Equals(TerminalCommandReference other) { return base.Equals(other); }
    }
}
