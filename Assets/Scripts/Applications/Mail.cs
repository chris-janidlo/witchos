using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : MonoBehaviour
{
    public Window Window;
    public Sprite ReadIcon, UnreadIcon;

    void Update ()
    {
        int unreadCount = MailState.Instance.UnreadMessageCount;
        Window.Icon = unreadCount > 0 ? UnreadIcon : ReadIcon;
        Window.Title = "Inbox" + (unreadCount > 0 ? $" ({unreadCount})" : "");
    }
}
