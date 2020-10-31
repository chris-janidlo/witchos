using UnityEngine;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Event Instancer of type `SpellDeliverable`. Inherits from `AtomEventInstancer&lt;SpellDeliverable, SpellDeliverableEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/SpellDeliverable Event Instancer")]
    public class SpellDeliverableEventInstancer : AtomEventInstancer<SpellDeliverable, SpellDeliverableEvent> { }
}
