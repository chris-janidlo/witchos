using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using crass;

public class Alert : Singleton<Alert>, IPointerClickHandler
{
    public float SecondsToDisplayPerWord;
    public TransitionableFloat FadeoutTransition;

    public TextMeshProUGUI Text;
    public CanvasGroup Group;

    LinkedList<string> messages = new LinkedList<string>();

    bool clickFlag;

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

                float timer = message.Split(' ').Length * SecondsToDisplayPerWord;
                
                while (timer > 0 && !clickFlag)
                {
                    timer -= Time.deltaTime;
                    yield return null;
                }

                FadeoutTransition.FlashFromTo(1, 0);

                do
                {
                    Group.alpha = FadeoutTransition.Value;
                    yield return null;
                }
                while (Group.alpha != 0);
            }

            clickFlag = false;
            yield return null;
        }
    }

    void Update ()
    {
        // enable clicking away when message is fully visible, while not blocking interaction with anything below the message if it's transparent at all
        Group.blocksRaycasts = Group.alpha == 1;
    }

	public void OnPointerClick (PointerEventData eventData)
	{
        clickFlag = true;
	}

    public void ClearMessages ()
    {
        messages.Clear();
    }

    public void ShowMessage (string message)
    {
        messages.AddLast(message);
    }

    public void ShowMessageNext (string message)
    {
        messages.AddFirst(message);
    }

#if UNITY_EDITOR
    [ContextMenu("Test Message (end of queue)")]
    public void TriggerTestMessage ()
    {
        ShowMessage("This is a test");
    }

    [ContextMenu("Test Message (start of queue)")]
    public void TriggerTestMessageNext ()
    {
        ShowMessageNext("This is a test");
    }
#endif // UNITY_EDITOR
}
