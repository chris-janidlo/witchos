using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

namespace WitchOS
{
    public class WikiApp : MonoBehaviour, IPointerUpHandler
    {
        public WikiPageData HomePage, PageNotFoundPage;

        public string BaseTitle;

        public WikiPageBuildingBlock LeadBuildingBlock, BodySectionBuildingBlock;

        public Window Window;

        public ScrollRect ScrollRect;

        public List<TextMeshProUGUI> PageTitleContainers;
        public VerticalLayoutGroup PageContentContainer;

        public Button NavigateBackButton, NavigateForwardButton;

#if UNITY_EDITOR
        public string DebugOpenPageID;
#endif // UNITY_EDITOR

        private class SessionDataPageSnapshot
        {
            public WikiPageData Page;
            public float ScrollPosition;

            public SessionDataPageSnapshot (WikiPageData page)
            {
                Page = page;
                ScrollPosition = 1; // 1 == top of page
            }
        }

        List<WikiPageBuildingBlock> buildingBlocksForCurrentPage = new List<WikiPageBuildingBlock>();

        List<SessionDataPageSnapshot> sessionBrowsingHistory;
        int currentPositionInSessionBrowsingHistory;

        void Start ()
        {
            sessionBrowsingHistory = new List<SessionDataPageSnapshot>();
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
            if (currentPositionInSessionBrowsingHistory >= 0) recordScrollRectPositionToHistory();

            if (currentPositionInSessionBrowsingHistory < sessionBrowsingHistory.Count - 1)
            {
                int cuttingPoint = currentPositionInSessionBrowsingHistory + 1;
                sessionBrowsingHistory.RemoveRange(cuttingPoint, sessionBrowsingHistory.Count - cuttingPoint);
            }

            currentPositionInSessionBrowsingHistory++;
            sessionBrowsingHistory.Add(new SessionDataPageSnapshot(page));

            renderPage(page);

            ScrollRect.verticalNormalizedPosition = 1;
        }

        // hook up the button to call this in the onClick callback
        public void NavigateBack ()
        {
            navigate(-1);
        }

        // hook up the button to call this in the onClick callback
        public void NavigateForward ()
        {
            navigate(+1);
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

        void navigate (int direction)
        {
            recordScrollRectPositionToHistory();

            currentPositionInSessionBrowsingHistory = Mathf.Clamp(currentPositionInSessionBrowsingHistory + direction, 0, sessionBrowsingHistory.Count);
            SessionDataPageSnapshot snapshot = sessionBrowsingHistory[currentPositionInSessionBrowsingHistory];
            
            renderPage(snapshot.Page);
            // force rebuild the layout so that setting the vertical scroll position of the scrollrect uses the actual, built layout
            LayoutRebuilder.ForceRebuildLayoutImmediate(ScrollRect.transform as RectTransform);
            ScrollRect.verticalNormalizedPosition = snapshot.ScrollPosition;
        }

        void recordScrollRectPositionToHistory ()
        {
            sessionBrowsingHistory[currentPositionInSessionBrowsingHistory].ScrollPosition = ScrollRect.verticalNormalizedPosition;
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
