using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WitchOS
{
    public class WindowOpener : MonoBehaviour
    {
        public Window ApplicationPrefab;
        public ScriptableObject ApplicationData;
        public bool OpenSingleInstance;

        public virtual Window Open ()
        {
            var options = WindowFactory.Options.TaskBarButton;
            if (OpenSingleInstance) options |= WindowFactory.Options.Singleton;

            return WindowFactory.Instance.OpenWindow(ApplicationPrefab, ApplicationData, options);
        }

        public void OpenForUnityEventCallback ()
        {
            Open();
        }
    }
}
