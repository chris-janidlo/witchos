using System;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    [CreateAssetMenu(fileName = "NewWikiPage.asset", menuName = "WitchOS/Wiki Page")]
    public class WikiPage : ScriptableObject
    {
        public static Dictionary<string, WikiPage> LookUpTable = new Dictionary<string, WikiPage>();

        [Serializable]
        public class ContentSection
        {
            public string Title;
            [TextArea(5, 100)]
            public string Content;
        }

        [TextArea(5, 100)]
        public string LeadSection;

        public List<ContentSection> BodySections;

        public string Title => name;

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
