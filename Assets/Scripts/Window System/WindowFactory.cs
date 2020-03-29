using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class WindowFactory : Singleton<WindowFactory>
{
    public TaskBarButton TaskBarButtonPrefab;
    public RectTransform WindowParent;

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    public void CreateWindowWithTaskbarButton (Window prefab)
    {
        var window = Instantiate(prefab, WindowParent);
        var button = Instantiate(TaskBarButtonPrefab);

        window.SetTaskBarButton(button);
        button.SetWindow(window);

        TaskBar.Instance.AddButton(button);

        window.Focus();
    }
}
