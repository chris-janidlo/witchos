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

    LinkedList<IEnumerator> enums = new LinkedList<IEnumerator>();
    IEnumerator currentEnum;

    void Awake ()
    {
        SingletonOverwriteInstance(this);
        FadeoutTransition.AttachMonoBehaviour(this);
        Group.alpha = 0;
    }

    void Update ()
    {
        // enable clicking away when message is fully visible, while not blocking interaction with anything below the message if it's transparent at all
        Group.blocksRaycasts = Group.alpha == 1;

        if (enums.Count > 0 && currentEnum == null)
        {
            StartCoroutine(currentEnum = enums.First.Value);
            enums.RemoveFirst();
        }
    }

	public void OnPointerClick (PointerEventData eventData)
	{
        hideImmediately();
	}

    public void ClearMessages ()
    {
        StopCoroutine(currentEnum);
        enums.Clear();
    }

    public void ShowMessage (string message)
    {
        enums.AddLast(showAndHideRoutine(message));
    }

    public void ShowMessageImmediately (string message)
    {
        hideImmediately();
        enums.AddFirst(showAndHideRoutine(message));
    }

#if UNITY_EDITOR
    [ContextMenu("Test Message (wait for every other pending message to show)")]
    public void TriggerTestMessage ()
    {
        ShowMessage("This is a test");
    }

    [ContextMenu("Test Message (show immediately, dismissing the current message)")]
    public void TriggerTestMessageNext ()
    {
        ShowMessageImmediately("This is a test");
    }
#endif // UNITY_EDITOR

    IEnumerator showAndHideRoutine (string message)
    {
        Text.text = message;
        Group.alpha = 1;

        yield return new WaitForSeconds(message.Split(' ').Length * SecondsToDisplayPerWord);

        yield return hideRoutine();
    }

    IEnumerator hideRoutine ()
    {
        FadeoutTransition.FlashFromTo(1, 0);

        do
        {
            Group.alpha = FadeoutTransition.Value;
            yield return null;
        }
        while (Group.alpha != 0);

        currentEnum = null;
    }

    void hideImmediately ()
    {
        StopCoroutine(currentEnum);
        StartCoroutine(hideRoutine());
    }
}
