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

        public List<TextMeshProUGUI> PageTitleContainers;

        public Button NavigateBackButton, NavigateForwardButton;

        public Window Window;
        public VerticalLayoutGroup PageContentContainer;

#if UNITY_EDITOR
        public string DebugOpenPageID;
#endif // UNITY_EDITOR

        List<WikiPageBuildingBlock> buildingBlocksForCurrentPage = new List<WikiPageBuildingBlock>();

        List<WikiPageData> sessionBrowsingHistory;
        int currentPositionInSessionBrowsingHistory;

        void Start ()
        {
            sessionBrowsingHistory = new List<WikiPageData>();
            currentPositionInSessionBrowsingHistory = -1; // initialize to -1 since it will get incremented in first OpenPage call

            OpenPage(HomePage);
        }

        void Update ()
        {
            if (!Window.Focused) return;

            if (Input.GetMouseButtonUp(3))
            {
                NavigateBack();
            }
            else if (Input.GetMouseButtonUp(4))
            {
                NavigateForward();
            }
        }

        public void OnPointerUp (PointerEventData eventData)
        {
            if (!Window.Focused) return;

            string linkID = buildingBlocksForCurrentPage.SingleOrDefault(b => b.Focused)?.GetHoveredLinkID();

            if (linkID == null) return;

            OpenPageID(linkID);
        }

        public void OpenPageID (string pageID)
        {
            OpenPage(SOLookupTable.Instance.GetAsset<WikiPageData>($"d2/{pageID}") ?? PageNotFoundPage);
        }

        public void OpenPage (WikiPageData page)
        {
            if (currentPositionInSessionBrowsingHistory < sessionBrowsingHistory.Count - 1)
            {
                int cuttingPoint = currentPositionInSessionBrowsingHistory + 1;
                sessionBrowsingHistory.RemoveRange(cuttingPoint, sessionBrowsingHistory.Count - cuttingPoint);
            }

            currentPositionInSessionBrowsingHistory++;
            sessionBrowsingHistory.Add(page);

            renderPage(page);
        }

        // hook up the button to call this in the onClick callback
        public void NavigateBack ()
        {
            currentPositionInSessionBrowsingHistory = Mathf.Max(0, currentPositionInSessionBrowsingHistory - 1);
            renderPage(sessionBrowsingHistory[currentPositionInSessionBrowsingHistory]);
        }

        // hook up the button to call this in the onClick callback
        public void NavigateForward ()
        {
            currentPositionInSessionBrowsingHistory = Mathf.Min(sessionBrowsingHistory.Count, currentPositionInSessionBrowsingHistory + 1);
            renderPage(sessionBrowsingHistory[currentPositionInSessionBrowsingHistory]);
        }

        void renderPage (WikiPageData page)
        {
            foreach (var block in buildingBlocksForCurrentPage)
            {
                Destroy(block.gameObject);
            }

            foreach (var titleContainer in PageTitleContainers)
            {
                titleContainer.text = page.Title;
            }

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

            NavigateForwardButton.interactable = currentPositionInSessionBrowsingHistory < sessionBrowsingHistory.Count - 1;
            NavigateBackButton.interactable = currentPositionInSessionBrowsingHistory > 0;
        }

#if UNITY_EDITOR
        [ContextMenu("Open the page pointed to by DebugOpenPageID")]
        void debugOpenPage ()
        {
            OpenPageID(DebugOpenPageID);
        }
#endif // UNITY_EDITOR
    }
}
