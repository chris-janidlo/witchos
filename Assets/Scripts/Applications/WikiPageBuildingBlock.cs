using System;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using crass;

namespace WitchOS
{
    public class WikiPageBuildingBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string LinkStyleOpenTag, LinkStyleCloseTag;

        public TextMeshProUGUI ContentTextDisplay;

        public bool Focused { get; private set; }

        public void OnPointerEnter (PointerEventData eventData)
        {
            Focused = true;
        }

        public void OnPointerExit (PointerEventData eventData)
        {
            Focused = false;
        }

        public virtual void SetContent (WikiPageData.ContentSection contentSection)
        {
            ContentTextDisplay.text = renderLinks(contentSection.Content);
        }

        public string GetHoveredLinkID ()
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(ContentTextDisplay, Input.mousePosition, CameraCache.Main);

            return linkIndex < 0 ? null : ContentTextDisplay.textInfo.linkInfo[linkIndex].GetLinkID();
        }

        string renderLinks (string bracketedText)
        {
            StringBuilder sb = new StringBuilder();

            bool scanningLink = false, sawDelimiter = false;
            string linkText = "", displayText = "";

            foreach (char c in bracketedText)
            {
                switch (c)
                {
                    case WikiPageData.LINK_BEGIN_TOKEN:
                        if (scanningLink)
                            throw new ArgumentException($"wiki page parser error: two {WikiPageData.LINK_BEGIN_TOKEN} tokens appeared before a matching {WikiPageData.LINK_END_TOKEN}");
                        scanningLink = true;
                        sawDelimiter = false;
                        break;

                    case WikiPageData.LINK_END_TOKEN:
                        if (!scanningLink)
                            throw new ArgumentException($"wiki page parser error: {WikiPageData.LINK_END_TOKEN} token appeared without a matching {WikiPageData.LINK_BEGIN_TOKEN}");
                        scanningLink = false;

                        sb.Append(tagLink(linkText, displayText == "" ? linkText : displayText));

                        linkText = "";
                        displayText = "";
                        break;

                    case WikiPageData.ALIAS_LINK_DELIMITER:
                        if (!scanningLink)
                            goto default;

                        if (sawDelimiter)
                            throw new ArgumentException($"wiki page parser error: saw more than one {WikiPageData.ALIAS_LINK_DELIMITER} token in a single link declaration");

                        sawDelimiter = true;

                        displayText = linkText;
                        linkText = "";
                        break;

                    default:
                        if (scanningLink)
                        {
                            linkText += c;
                        }
                        else
                        {
                            sb.Append(c);
                        }

                        break;
                }
            }

            return sb.ToString();
        }

        string tagLink (string linkText, string displayText)
        {
            return $"{LinkStyleOpenTag}<link=\"{linkText}\">{displayText}</link>{LinkStyleCloseTag}";
        }
    }
}
