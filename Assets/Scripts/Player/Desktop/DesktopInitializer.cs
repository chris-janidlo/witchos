using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class DesktopInitializer : MonoBehaviour
    {
        public DirectoryDrawer DesktopDrawer;
        public Filesystem Filesystem;

        void Start ()
        {
            Filesystem.Initialize();
            DesktopDrawer.Draw(Filesystem.RootDirectory);
        }
    }
}
