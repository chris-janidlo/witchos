using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace WitchOS
{
    public class WikiApp : MonoBehaviour, IPointerUpHandler
    {
        public WikiPageData HomePage, PageNotFoundPage;

        public string BaseTitle;

        public WikiPageBuildingBlock LeadBuildingBlock, BodySectionBuildingBlock;

        public TextMeshProUGUI PageTitleContainer;

        public Window Window;
        public VerticalLayoutGroup PageContentContainer;

        List<WikiPageBuildingBlock> buildingBlocksForCurrentPage = new List<WikiPageBuildingBlock>();

        void Start ()
        {
            renderPage(HomePage);
        }

        public void OnPointerUp (PointerEventData eventData)
        {
            if (!Window.Focused) return;

            string linkID = buildingBlocksForCurrentPage.SingleOrDefault(b => b.Focused)?.GetHoveredLinkID();

            if (linkID == null) return;

            renderPage(SOLookupTable.Instance.GetAsset<WikiPageData>($"d2/{linkID}") ?? PageNotFoundPage);
        }

        void renderPage (WikiPageData page)
        {
            foreach (var block in buildingBlocksForCurrentPage)
            {
                Destroy(block.gameObject);
            }

            PageTitleContainer.text = page.Title;
            Window.Title = $"{page.Title} - {BaseTitle}";

            buildingBlocksForCurrentPage = new List<WikiPageBuildingBlock>();

            Transform blockParent = PageContentContainer.transform;

            var leadBlock = Instantiate(LeadBuildingBlock, blockParent);
            leadBlock.SetContent(page.LeadSection);
            buildingBlocksForCurrentPage.Add(leadBlock);

            foreach (var bodySection in page.BodySections)
            {
                var bodyBlock = Instantiate(BodySectionBuildingBlock, blockParent);
                bodyBlock.SetContent(bodySection);
                buildingBlocksForCurrentPage.Add(bodyBlock);
            }
        }
    }
}
