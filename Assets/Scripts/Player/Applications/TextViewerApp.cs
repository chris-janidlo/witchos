using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WitchOS
{
    public class TextViewerApp : MonoBehaviour
    {
        public Window Window;

        public TextMeshProUGUI ContentText, PageNumberIndicator;
        public ScrollRect ScrollRect;

        public Button NextPage, PreviousPage;

        List<string> pages;
        int pageNum;

        void Start ()
        {
            NextPage.onClick.AddListener(() => incrementPage(1));
            PreviousPage.onClick.AddListener(() => incrementPage(-1));
        }

        public void SetData ()
        {
            pages = (Window.File as TextPDFFile).Data.Pages;
            setPage(1);
        }

        void incrementPage (int direction)
        {
            ScrollRect.verticalNormalizedPosition = 1;
            setPage(pageNum + (int) Mathf.Sign(direction));
        }

        void setPage (int target)
        {
            target = Mathf.Clamp(target, 1, pages.Count);
            if (target == pageNum) return;

            pageNum = target;

            ContentText.text = pages[pageNum - 1];
            PageNumberIndicator.text = pages.Count == 1 ? "" : pageNum.ToString();

            NextPage.interactable = pageNum != pages.Count;
            PreviousPage.interactable = pageNum != 1;
        }
    }
}
