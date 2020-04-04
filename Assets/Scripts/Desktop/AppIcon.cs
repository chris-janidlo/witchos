using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppIcon : MonoBehaviour
{
    public event Action<Window> WindowOpened;

    public Window WindowPrefab;
    public bool OnlyOpenOnce;

    public void LaunchApplication ()
    {
        var window = OnlyOpenOnce
            ? WindowFactory.Instance.CreateSingletonWindowWithTaskbarButton(WindowPrefab)
            : WindowFactory.Instance.CreateWindowWithTaskbarButton(WindowPrefab);

        WindowOpened?.Invoke(window);
    }
}
