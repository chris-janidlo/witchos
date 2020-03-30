using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailMessageWindow : MonoBehaviour
{
    public Window Window;
    public TextMeshProUGUI ContentText;

    MailState.PlaceholderMailMessage message;

    void Update ()
    {
        Window.Title = message.SenderAddress + " - " + message.Subject;
        ContentText.text = message.Subject + "\n-------------\n\n" + message.Body;
    }

    public void SetMessage (MailState.PlaceholderMailMessage message)
    {
        this.message = message;
    }
}
