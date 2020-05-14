using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewInvoice.asset", menuName = "Invoice")]
public class Invoice : ScriptableObject
{
    public UnityEvent Completed;

    public Casting SpellRequest;
    public string BuyerName;
    [TextArea(5, 100)]
    public string Justification;

    public bool Read { get; set; }

    public override int GetHashCode ()
    {
        return (BuyerName + SpellRequest.ToString() + Justification).GetHashCode();
    }
}
