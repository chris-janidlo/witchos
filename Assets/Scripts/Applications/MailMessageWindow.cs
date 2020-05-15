using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailMessageWindow : MonoBehaviour
{
    public Window Window;
    public TextMeshProUGUI ContentText;

    EMail message;

    void Update ()
    {
        Window.Title = message.Subject;
        ContentText.text = "Subject: " + message.Subject + "\n\n" + message.Body;
    }

    public void SetMessage (EMail message)
    {
        this.message = message;
    }
}
