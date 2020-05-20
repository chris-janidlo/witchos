using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyManagerApp : MonoBehaviour
{
    public VerticalLayoutGroup InvoicePreviewContainer;
    public OrderPreview InvoicePreviewPrefab;

    void Start ()
    {
        foreach (var item in OrderState.Instance.InvoiceInbox.Entries)
        {
            Instantiate(InvoicePreviewPrefab, InvoicePreviewContainer.transform).SetInvoice(item.Value);
        }
    }
}
