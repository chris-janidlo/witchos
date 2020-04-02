using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

// TODO: reduce code duplication between the two versions of create singleton window. maybe use flags pattern?
public class WindowFactory : Singleton<WindowFactory>
{
    public TaskBarButton TaskBarButtonPrefab;
    public RectTransform WindowParent;

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    public Window CreateWindow (Window prefab)
    {
        var window = Instantiate(prefab, WindowParent);
        window.Focus();

        return window;
    }

    public Window CreateSingletonWindow (Window prefab, string name)
    {
        Window window = GameObject.Find(name)?.GetComponent<Window>();

        if (window != null)
        {
            window.Focus();
            return window;
        }

        window = CreateWindow(prefab);
        window.name = name;
        return window;
    }

    public Window CreateSingletonWindow (Window prefab)
    {
        return CreateSingletonWindow(prefab, prefab.name);
    }

    public Window CreateWindowWithTaskbarButton (Window prefab)
    {
        var window = CreateWindow(prefab);

        var button = Instantiate(TaskBarButtonPrefab);

        window.SetTaskBarButton(button);
        button.SetWindow(window);

        TaskBar.Instance.AddButton(button);

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
