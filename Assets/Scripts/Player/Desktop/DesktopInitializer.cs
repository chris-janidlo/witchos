using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class DesktopInitializer : MonoBehaviour
    {
        public DirectoryDrawer DesktopDrawer;
        public FileSystem FileSystem;

        void Start ()
        {
            FileSystem.Initialize();
            DesktopDrawer.Draw(FileSystem.RootDirectory);
        }
    }
}
