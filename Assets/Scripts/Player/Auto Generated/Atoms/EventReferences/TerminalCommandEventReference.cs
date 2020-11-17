using System;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event Reference of type `TerminalCommand`. Inherits from `AtomEventReference&lt;TerminalCommand, TerminalCommandVariable, TerminalCommandEvent, TerminalCommandVariableInstancer, TerminalCommandEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class TerminalCommandEventReference : AtomEventReference<
        TerminalCommand,
        TerminalCommandVariable,
        TerminalCommandEvent,
        TerminalCommandVariableInstancer,
        TerminalCommandEventInstancer>, IGetEvent 
    { }
}
