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

    MailState.PlaceholderMailMessage message;

    void Start ()
    {
        Button.onClick.AddListener(onClick);
    }

    void Update ()
    {
        Label.text = (message.Read ? "" : "* ") + message.SenderAddress + " - " + message.Subject;
    }

    public void SetMessage (MailState.PlaceholderMailMessage message)
    {
        this.message = message;
    }

    void onClick ()
    {
        message.Read = true;

        WindowFactory.Instance
            .CreateSingletonWindowWithTaskbarButton(MessageWindowPrefab, "mail message " + message.GetHashCode().ToString())
            .GetComponent<MailMessageWindow>().SetMessage(message);
    }
}
