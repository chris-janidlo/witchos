using UnityEngine;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Set variable value Action of type `Rite`. Inherits from `SetVariableValue&lt;Rite, RitePair, RiteVariable, RiteConstant, RiteReference, RiteEvent, RitePairEvent, RiteVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Rite", fileName = "SetRiteVariableValue")]
    public sealed class SetRiteVariableValue : SetVariableValue<
        Rite,
        RitePair,
        RiteVariable,
        RiteConstant,
        RiteReference,
        RiteEvent,
        RitePairEvent,
        RiteRiteFunction,
        RiteVariableInstancer>
    { }
}
