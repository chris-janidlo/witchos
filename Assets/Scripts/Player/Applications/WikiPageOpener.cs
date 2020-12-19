using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class WikiPageOpener : MonoBehaviour
    {
        public WikiPageData Page;

        public void Open ()
        {
            // reference equality means that this won't cause old instances of the wiki with this file open to refocus
            var file = new WikiFile
            {
                Name = Page.name,
                Data = new SaveableWikiPageDataReference { Value = Page }
            };

            WindowFactory.Instance.OpenWindowWithFile(file);
        }
    }
}
