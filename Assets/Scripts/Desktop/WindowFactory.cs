using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
    public class WindowFactory : Singleton<WindowFactory>
    {
        public TaskBarButton TaskBarButtonPrefab;
        public RectTransform WindowParent;

        [Flags]
        public enum Options
        {
            None = 0,
            Singleton = 1,
            TaskBarButton = 2
        }

        void Awake ()
        {
            SingletonOverwriteInstance(this);
        }

        public Window OpenWindow (Window prefab, ScriptableObject data, string name, Options options = Options.None)
        {
            Window window = null;

            if (options.HasFlag(Options.Singleton))
            {
                window = GameObject.Find(name)?.GetComponent<Window>();
            }

            if (window == null)
            {
                // either we're not doing a singleton or a singleton wasn't found

                window = Instantiate(prefab, WindowParent);
                window.name = name;
                window.AppData = data;

                if (options.HasFlag(Options.TaskBarButton))
                {
                    var button = Instantiate(TaskBarButtonPrefab);

                    window.SetTaskBarButton(button);
                    button.SetWindow(window);

                    TaskBar.Instance.AddButton(button);
                }
            }

            window.Focus();
            return window;
        }

        public Window OpenWindow (Window prefab, ScriptableObject data, Options options = Options.None)
        {
            return OpenWindow(prefab, data, prefab.name + data?.name ?? "", options);
        }

        public Window OpenWindow (Window prefab, string name, Options options = Options.None)
        {
            return OpenWindow(prefab, null, name, options);
        }

        public Window OpenWindow (Window prefab, Options options = Options.None)
        {
            return OpenWindow(prefab, null, prefab.name, options);
        }
    }
}
