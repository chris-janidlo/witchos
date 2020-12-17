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
            Label.text = (entry.Read ? "" : "* ") + entry.Contents.AnnotatedSubject + " - " + entry.Contents.EmailData.Value.SenderAddress;
        }

        public void SetMailEntry (MailState.Entry entry)
        {
            this.entry = entry;
        }

        void onClick ()
        {
            entry.Read = true;

            FileBase file;

            if (entry.Contents is Order)
            {
                file = new File<Order> { Data = entry.Contents as Order };
            }
            else
            {
                file = new File<Email> { Data = entry.Contents };
            }

            file.Name = entry.Contents.AnnotatedSubject;

            WindowFactory.Instance.OpenWindowWithFile(file);
        }
    }
}
