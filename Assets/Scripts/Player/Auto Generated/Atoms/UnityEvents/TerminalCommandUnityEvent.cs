using System;
using UnityEngine.Events;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// None generic Unity Event of type `TerminalCommand`. Inherits from `UnityEvent&lt;TerminalCommand&gt;`.
    /// </summary>
    [Serializable]
    public sealed class TerminalCommandUnityEvent : UnityEvent<TerminalCommand> { }
}
