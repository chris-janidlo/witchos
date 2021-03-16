using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event Instancer of type `TerminalCommandPair`. Inherits from `AtomEventInstancer&lt;TerminalCommandPair, TerminalCommandPairEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/TerminalCommandPair Event Instancer")]
    public class TerminalCommandPairEventInstancer : AtomEventInstancer<TerminalCommandPair, TerminalCommandPairEvent> { }
}
