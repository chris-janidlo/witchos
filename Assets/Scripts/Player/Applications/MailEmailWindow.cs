using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms;
using TMPro;

namespace WitchOS
{
    public class MailEmailWindow : MonoBehaviour
    {
        public const string SEPARATOR = "-------------";

        public Window Window;
        public TextMeshProUGUI ContentText;

        void Start ()
        {
            var message = Window.File.GetData<Email>();

            Window.Title = message.AnnotatedSubject;
            ContentText.text = makeEmailText(message);
        }

        protected string makeEmailText (Email email)
        {
            return $"Subject: {email.SubjectLine}\n{SEPARATOR}\n\n{email.Body}";
        }
    }
}
