using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewInvoice.asset", menuName = "Invoice")]
public class Invoice : ScriptableObject
{
    public UnityEvent Completed;

    public Spell RequestedSpell;
    public string BuyerAddress, EmailSubjectLine;
    [TextArea]
    public string Justification;

    public bool Read { get; set; }

    public override int GetHashCode ()
    {
        return (BuyerAddress + RequestedSpell.ToString() + Justification).GetHashCode();
    }
}
