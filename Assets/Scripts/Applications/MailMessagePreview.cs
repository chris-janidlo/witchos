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

    EMail message;

    void Start ()
    {
        Button.onClick.AddListener(onClick);
    }

    void Update ()
    {
        Label.text = (message.Read ? "" : "* ") + message.FromAddress + " - " + message.Subject;
    }

    public void SetMessage (EMail message)
    {
        this.message = message;
    }

    void onClick ()
    {
        message.Read = true;

        WindowFactory.Instance
            .OpenWindow(MessageWindowPrefab, "mail message " + message.GetHashCode().ToString(), WindowFactory.Options.Singleton | WindowFactory.Options.TaskBarButton)
            .GetComponent<MailMessageWindow>().SetMessage(message);
    }
}
