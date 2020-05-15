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
        if (MailState.Instance.Inbox.Count != previousMailCount)
        {
            populateInbox();
        }

        previousMailCount = MailState.Instance.Inbox.Count;

        int unreadCount = MailState.Instance.Inbox.UnreadCount;
        Window.Icon = unreadCount > 0 ? UnreadIcon : ReadIcon;
        Window.Title = "Inbox" + (unreadCount > 0 ? $" ({unreadCount} unread)" : "");
    }

    void populateInbox ()
    {
        foreach (Transform child in InboxContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var message in MailState.Instance.Inbox.Entries)
        {
            Instantiate(PreviewPrefab, InboxContainer.transform).SetMessage(message.Value);
        }
    }
}
