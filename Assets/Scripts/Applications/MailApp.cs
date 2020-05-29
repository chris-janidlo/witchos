using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailApp : MonoBehaviour
{
    public Window Window;
    public Sprite ReadIcon, UnreadIcon;

    public MailMessagePreview PreviewPrefab;
    public VerticalLayoutGroup InboxContainer;

    int previousMailCount;

    void Update ()
    {
        if (MailState.Instance.CurrentMessages.Count != previousMailCount)
        {
            populateInbox();
        }

        previousMailCount = MailState.Instance.CurrentMessages.Count;

        int unreadCount = MailState.Instance.UnreadMessageCount;
        Window.Icon = unreadCount > 0 ? UnreadIcon : ReadIcon;
        Window.Title = "Inbox" + (unreadCount > 0 ? $" ({unreadCount} unread)" : "");
    }

    void populateInbox ()
    {
        foreach (Transform child in InboxContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Invoice message in MailState.Instance.CurrentMessages)
        {
            Instantiate(PreviewPrefab, InboxContainer.transform).SetMessage(message);
        }
    }
}
