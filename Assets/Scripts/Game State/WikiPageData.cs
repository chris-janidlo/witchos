using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace WitchOS
{
    [CreateAssetMenu(fileName = "NewWikiPage.asset", menuName = "WitchOS/Wiki Page")]
    public class WikiPageData : ScriptableObject
    {
        public const string
            // raw links link to the page named between the tokens. the text of the link is exactly the same as the page name
            RAW_LINK_BEGIN_TOKEN = "[",
            RAW_LINK_END_TOKEN = "]",
            // alias links display the tex before the delimiter, but link to the page named after the delimiter
            ALIAS_LINK_BEGIN_TOKEN = "{",
            ALIAS_LINK_END_TOKEN = "}",
            ALIAS_LINK_DELIMITER = "->";

        public static Dictionary<string, WikiPageData> LookUpTable = new Dictionary<string, WikiPageData>();

        [Serializable]
        public class ContentSection
        {
            [TextArea(5, 100)]
            public string Content;
        }

        [Serializable]
        public class BodyContentSection : ContentSection
        {
            public string Title;
        }

        public string Title;

        public ContentSection LeadSection;

        public List<BodyContentSection> BodySections;

        // TODO need to test this as a lifecycle thing
        void OnEnable ()
        {
            if (LookUpTable.ContainsKey(Title))
            {
                throw new InvalidOperationException($"attempted to add two pages with the same title ({Title})");
            }

            LookUpTable[Title] = this;
        }
    }
}
