using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mail : MonoBehaviour
{
    public Window Window;
    public Sprite ReadIcon, UnreadIcon;

    public MailMessagePreview PreviewPrefab;
    public VerticalLayoutGroup InboxContainer;

    int previousMailCount;

    void Update ()
    {
        if (MailState.Instance.Messages.Count != previousMailCount)
        {
            populateInbox();
        }

        previousMailCount = MailState.Instance.Messages.Count;

        int unreadCount = MailState.Instance.UnreadMessageCount;
        Window.Icon = unreadCount > 0 ? UnreadIcon : ReadIcon;
        Window.Title = "Inbox" + (unreadCount > 0 ? $" ({unreadCount})" : "");
    }

    void populateInbox ()
    {
        foreach (Transform child in InboxContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (MailState.PlaceholderMailMessage message in MailState.Instance.Messages)
        {
            Instantiate(PreviewPrefab, InboxContainer.transform).SetMessage(message);
        }
    }
}
