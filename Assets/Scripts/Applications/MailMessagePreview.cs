using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MailMessagePreview : MonoBehaviour
{
    public Window MessageWindowPrefab;
    public Button Button;
    public TextMeshProUGUI Label;

    MailState.Entry entry;

    void Start ()
    {
        Button.onClick.AddListener(onClick);
    }

    void Update ()
    {
        Label.text = (entry.Read ? "" : "* ") + entry.Contents.BuyerAddress + " - " + entry.Contents.EmailSubjectLine;
    }

    public void SetMailEntry (MailState.Entry entry)
    {
        this.entry = entry;
    }

    void onClick ()
    {
        entry.Read = true;

        WindowFactory.Instance
            .OpenWindow(MessageWindowPrefab, "mail message " + entry.GetHashCode().ToString(), WindowFactory.Options.Singleton | WindowFactory.Options.TaskBarButton)
            .GetComponent<MailMessageWindow>().SetMessage(entry.Contents);
    }
}
