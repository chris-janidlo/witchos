using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace WitchOS
{
    [CreateAssetMenu(fileName = "NewWikiPage.asset", menuName = "WitchOS/Wiki Page")]
    public class WikiPageData : ScriptableObject
    {
        public const char
            // all links begin and end with the following characters, respectively
            LINK_BEGIN_TOKEN = '{',
            LINK_END_TOKEN = '}',
            // "raw" links are just those characters containing the name of the page to link to. the displayed text of the link will also be the page name
            // "alias" links specify the text to use as the display, in {display:link} format
            ALIAS_LINK_DELIMITER = ':';

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

        // TODO put this somewhere else. maybe another editor script
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
