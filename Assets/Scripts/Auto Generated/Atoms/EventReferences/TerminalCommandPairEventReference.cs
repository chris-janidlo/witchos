using System;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event Reference of type `TerminalCommandPair`. Inherits from `AtomEventReference&lt;TerminalCommandPair, TerminalCommandVariable, TerminalCommandPairEvent, TerminalCommandVariableInstancer, TerminalCommandPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class TerminalCommandPairEventReference : AtomEventReference<
        TerminalCommandPair,
        TerminalCommandVariable,
        TerminalCommandPairEvent,
        TerminalCommandVariableInstancer,
        TerminalCommandPairEventInstancer>, IGetEvent 
    { }
}
