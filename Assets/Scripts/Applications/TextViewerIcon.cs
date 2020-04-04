using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextViewerIcon : MonoBehaviour
{
    [TextArea]
    public List<string> Pages;
    public AppIcon AppIcon;
    public DesktopIcon DesktopIcon;

    void Start ()
    {
        AppIcon.WindowOpened += windowOpened;
    }

    void windowOpened (Window window)
    {
        window.GetComponent<TextViewerApp>().SetPages(Pages);
        window.Title = DesktopIcon.Label + " (readonly)";
    }
}
