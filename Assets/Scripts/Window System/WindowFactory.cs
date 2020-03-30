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

    public Window CreateWindowWithTaskbarButton (Window prefab)
    {
        var window = Instantiate(prefab, WindowParent);
        var button = Instantiate(TaskBarButtonPrefab);

        window.SetTaskBarButton(button);
        button.SetWindow(window);

        TaskBar.Instance.AddButton(button);

        window.Focus();

        return window;
    }

    public Window CreateSingletonWindowWithTaskbarButton (Window prefab, string name)
    {
        Window window = GameObject.Find(name)?.GetComponent<Window>();

        if (window != null)
        {
            window.Focus();
            return window;
        }

        window = CreateWindowWithTaskbarButton(prefab);
        window.name = name;
        return window;
    }

    public Window CreateSingletonWindowWithTaskbarButton (Window prefab)
    {
        return CreateSingletonWindowWithTaskbarButton(prefab, prefab.name);
    }
}
