using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class DirectoryApp : MonoBehaviour
    {
        public Window Window;
        public DirectoryDrawer DirectoryDrawer;

        void Start ()
        {
            DirectoryDrawer.Initialize(Window.File as Directory);
        }
    }
}
