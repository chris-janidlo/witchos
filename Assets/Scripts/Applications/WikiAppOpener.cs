using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class WikiAppOpener : WindowOpener
    {
        public WikiPageData Page;

        public override Window Open ()
        {
            Window window = base.Open();
            window.GetComponent<WikiApp>().OpenPage(Page);
            return window;
        }
    }
}
