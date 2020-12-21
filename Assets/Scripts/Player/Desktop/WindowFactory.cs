using System;
using System.Linq;
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

        public FileAssociationConfig FileAssociationConfig;

        void Awake ()
        {
            SingletonOverwriteInstance(this);
        }

        public Window OpenWindowWithFile (FileBase file)
        {
            var windowMetadata = FileAssociationConfig.GetMetadataForFile(file);

            Window window;
            switch (windowMetadata.NewWindowMode)
            {
                case WindowMetadata.NewWindowBehavior.FocusOldWindow:
                    window = findWindowWithMetadata(windowMetadata);
                    break;

                case WindowMetadata.NewWindowBehavior.OpenOneWindowPerFile:
                    window = findWindowWithFile(file);
                    break;

                case WindowMetadata.NewWindowBehavior.AlwaysOpenNewWindow:
                    window = null;
                    break;

                default:
                    throw new InvalidOperationException($"file association config {FileAssociationConfig.name} has unsupported window mode {windowMetadata.NewWindowMode} configured for file type {file.GetTypeOfData().Name}");
            }

            if (window == null)
            {
                window = Instantiate(windowMetadata.WindowPrefab, WindowParent);
                window.SetFile(file);

                if (windowMetadata.AddButtonToTaskbar)
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

        Window findWindowWithMetadata (WindowMetadata metadata)
        {
            return FindObjectsOfType<Window>().SingleOrDefault(w => FileAssociationConfig.GetMetadataForFile(w.File) == metadata);
        }

        Window findWindowWithFile (FileBase file)
        {
            return FindObjectsOfType<Window>().SingleOrDefault(w => w.File == file);
        }
    }
}
