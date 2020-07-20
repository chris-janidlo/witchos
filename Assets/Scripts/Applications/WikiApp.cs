using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using crass;

namespace WitchOS
{
    public class WikiApp : MonoBehaviour, IPointerUpHandler
    {
        public WikiPage HomePage;

        public Window Window;
        public TextMeshProUGUI PageText;

        void Start ()
        {
            drawPage(HomePage);
        }

        public void OnPointerUp (PointerEventData eventData)
        {
            if (!Window.Focused) return;

            int linkIndex = TMP_TextUtilities.FindIntersectingLink(PageText, Input.mousePosition, CameraCache.Main);

            if (linkIndex < 0) return;

            string linkID = PageText.textInfo.linkInfo[linkIndex].GetLinkID();
            drawPage(WikiPage.LookUpTable[linkID]);
        }

        void drawPage (WikiPage page)
        {
            // TODO build page from prefab building blocks
        }
    }
}
