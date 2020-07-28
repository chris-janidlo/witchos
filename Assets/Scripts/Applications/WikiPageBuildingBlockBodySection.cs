using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using crass;

namespace WitchOS
{
    public class WikiPageBuildingBlockBodySection : WikiPageBuildingBlock
    {
        public TextMeshProUGUI TitleDisplay;

        public override void SetContent (WikiPageData.ContentSection contentSection)
        {
            base.SetContent(contentSection);

            TitleDisplay.text = (contentSection as WikiPageData.BodyContentSection)?.Title;
        }
    }
}
