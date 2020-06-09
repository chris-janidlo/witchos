using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WitchOS
{
public class WindowOpenerButton : MonoBehaviour
{
    public Window ApplicationPrefab;
    public ScriptableObject ApplicationData;
    public bool OpenSingleInstance;
    public WindowEvent OnApplicationOpened;

    public Button Button;

    void Start ()
    {
        Button.onClick.AddListener(onClick);
    }

    void onClick ()
    {
        var options = WindowFactory.Options.TaskBarButton;
        if (OpenSingleInstance) options |= WindowFactory.Options.Singleton;

        var application = WindowFactory.Instance.OpenWindow(ApplicationPrefab, ApplicationData, options);

        OnApplicationOpened.Invoke(application);
    }
}
}
