using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WitchOS
{
public class MailMessagePreview : MonoBehaviour
{
    public Window EmailWindowPrefab, OrderWindowPrefab;
    public Button Button;
    public TextMeshProUGUI Label;

    MailState.Entry entry;

    void Start ()
    {
        Button.onClick.AddListener(onClick);
    }

    void Update ()
    {
        // TODO: when moving to atoms implementation, make this not poll-y
        Label.text = (entry.Read ? "" : "* ") + entry.Contents.EmailData.SenderAddress + " - " + entry.Contents.EmailData.SubjectLine;
    }

    public void SetMailEntry (MailState.Entry entry)
    {
        this.entry = entry;
    }

    void onClick ()
    {
        entry.Read = true;

        WindowFactory.Instance
            .OpenWindow
            (
                (entry.Contents is Order) ? OrderWindowPrefab : EmailWindowPrefab,
                "mail message " + entry.GetHashCode().ToString(),
                WindowFactory.Options.Singleton | WindowFactory.Options.TaskBarButton
            )
            .GetComponent<MailEmailWindow>().SetMessage(entry.Contents); // can use the same line for both cases bc order window is special case of email window
    }
}
}
