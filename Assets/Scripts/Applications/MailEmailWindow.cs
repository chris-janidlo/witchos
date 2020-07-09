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

        protected Email message;

        public void SetMessage (Email message)
        {
            this.message = message;

            Window.Title = message.AnnotatedSubject;
            ContentText.text = makeContentText();
        }

        protected virtual string makeContentText ()
        {
            return $"Subject: {message.EmailData.Value.SubjectLine}\n{SEPARATOR}\n\n{message.EmailData.Value.Body}";
        }
    }
}
