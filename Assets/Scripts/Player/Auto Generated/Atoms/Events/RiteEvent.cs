using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event of type `Rite`. Inherits from `AtomEvent&lt;Rite&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Rite", fileName = "RiteEvent")]
    public sealed class RiteEvent : AtomEvent<Rite> { }
}
