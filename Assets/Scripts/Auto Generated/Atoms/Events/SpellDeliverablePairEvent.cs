using UnityEngine;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `SpellDeliverablePair`. Inherits from `AtomEvent&lt;SpellDeliverablePair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/SpellDeliverablePair", fileName = "SpellDeliverablePairEvent")]
    public sealed class SpellDeliverablePairEvent : AtomEvent<SpellDeliverablePair> { }
}
