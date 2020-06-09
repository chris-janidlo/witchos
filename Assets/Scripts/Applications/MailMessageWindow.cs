using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WitchOS
{
public class MailMessageWindow : MonoBehaviour
{
    public Window Window;
    public TextMeshProUGUI ContentText;

    Invoice message;

    void Update ()
    {
        Window.Title = message.EmailSubjectLine;
        ContentText.text = "Subject: " + message.EmailSubjectLine +
            "\n-------------\nCurse: " + message.SpellRequest.Type +
            "\n-------------\nTarget: " + message.SpellRequest.TargetName +
            "\n-------------\n\n" + message.Justification;
    }

    public void SetMessage (Invoice message)
    {
        this.message = message;
        message.Completed.AddListener(Window.Close);
    }
}
}
