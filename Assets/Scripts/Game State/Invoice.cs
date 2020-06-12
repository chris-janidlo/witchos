using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WitchOS
{
[CreateAssetMenu(fileName = "NewInvoice.asset", menuName = "Invoice")]
public class Invoice : ScriptableObject
{
    public UnityEvent Completed;

    public SpellDeliverable SpellRequest;
    public string BuyerAddress, EmailSubjectLine;
    [TextArea(5, 100)]
    public string Justification;

    public override int GetHashCode ()
    {
        return (BuyerAddress + SpellRequest.ToString() + Justification).GetHashCode();
    }
}
}
