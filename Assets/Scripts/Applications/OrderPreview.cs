using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderPreview : MonoBehaviour
{
    public Sprite FlashOnSprite, FlashOffSprite, NormalSprite;
    public float FlashSecondsPerSprite;

    public Window DetailsPrefab;

    public TextMeshProUGUI Buyer, ItemCount, DueDate, TotalValue;

    public Button Button;
    public Image ButtonImage;

    Invoice invoice;

    float flashTimer;
    bool flashState;

    void Start ()
    {
        Button.onClick.AddListener(openInvoice);
    }

    void Update ()
    {
        bool read = OrderState.Instance.InvoiceInbox.GetReadState(invoice);

        if (read)
        {
            ButtonImage.sprite = NormalSprite;
        }
        else
        {
            flashTimer -= Time.deltaTime;

            if (flashTimer <= 0)
            {
                flashState = !flashState;
                flashTimer = FlashSecondsPerSprite;
            }

            ButtonImage.sprite = flashState ? FlashOnSprite : FlashOffSprite;
        }
    }

    public void SetInvoice (Invoice invoice)
    {
        this.invoice = invoice;

        Buyer.text = invoice.BuyerName;
        ItemCount.text = "1 item";
        DueDate.text = "No Due Date";
        TotalValue.text = "Gratis";
    }

    void openInvoice ()
    {
        WindowFactory.Instance.OpenWindow(DetailsPrefab, invoice, "invoice " + invoice.GetHashCode().ToString(), WindowFactory.Options.Singleton | WindowFactory.Options.TaskBarButton);
        OrderState.Instance.InvoiceInbox.SetReadState(invoice, true);
    }
}
