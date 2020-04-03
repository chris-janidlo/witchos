using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using crass;

public class Alert : Singleton<Alert>
{
    public float SecondsToDisplayPerWord;
    public TransitionableFloat FadeoutTransition;

    public TextMeshProUGUI Text;
    public CanvasGroup Group;

    LinkedList<string> messages = new LinkedList<string>();

    void Awake ()
    {
        SingletonOverwriteInstance(this);
        FadeoutTransition.AttachMonoBehaviour(this);
        Group.alpha = 0;
    }

    IEnumerator Start ()
    {
        while (true)
        {
            while (messages.Count != 0)
            {
                string message = messages.First.Value;
                messages.RemoveFirst();

                Text.text = message;
                Group.alpha = 1;

                yield return new WaitForSeconds(message.Split(' ').Length * SecondsToDisplayPerWord);

                FadeoutTransition.FlashFromTo(1, 0);

                do
                {
                    Group.alpha = FadeoutTransition.Value;
                    yield return null;
                }
                while (Group.alpha != 0);
            }

            yield return null;
        }
    }

    public void ShowMessage (string message)
    {
        messages.AddLast(message);
    }

    public void ShowMessageNext (string message)
    {
        messages.AddFirst(message);
    }
}
