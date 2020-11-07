using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event Instancer of type `TerminalCommand`. Inherits from `AtomEventInstancer&lt;TerminalCommand, TerminalCommandEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/TerminalCommand Event Instancer")]
    public class TerminalCommandEventInstancer : AtomEventInstancer<TerminalCommand, TerminalCommandEvent> { }
}
