using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event Instancer of type `Rite`. Inherits from `AtomEventInstancer&lt;Rite, RiteEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/Rite Event Instancer")]
    public class RiteEventInstancer : AtomEventInstancer<Rite, RiteEvent> { }
}
