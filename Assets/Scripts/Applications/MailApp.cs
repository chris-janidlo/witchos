using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WitchOS
{
public class MailApp : MonoBehaviour
{
    public Window Window;
    public Sprite ReadIcon, UnreadIcon;

    public MailMessagePreview PreviewPrefab;
    public VerticalLayoutGroup InboxContainer;

    int previousMailCount;

    void Update ()
    {
        if (MailState.Instance.CurrentMailEntries.Count != previousMailCount)
        {
            populateInbox();
        }

        previousMailCount = MailState.Instance.CurrentMailEntries.Count;

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

        foreach (MailState.Entry entry in MailState.Instance.CurrentMailEntries.Reverse())
        {
            Instantiate(PreviewPrefab, InboxContainer.transform).SetMailEntry(entry);
        }
    }
}
}
