using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using crass;

namespace WitchOS
{
    public class WikiPageBuildingBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string RawLinkOpenTag, RawLinkCloseTag;
        public string AliasLinkOpenTag, AliasLinkCloseTag, AliasLinkDelimiterTag;

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

        // TODO fix this. raw links can't work
        string renderLinks (string bracketedText)
        {
            return bracketedText
                .Replace(WikiPageData.RAW_LINK_BEGIN_TOKEN, RawLinkOpenTag)
                .Replace(WikiPageData.RAW_LINK_END_TOKEN, RawLinkCloseTag)
                .Replace(WikiPageData.ALIAS_LINK_BEGIN_TOKEN, AliasLinkOpenTag)
                .Replace(WikiPageData.ALIAS_LINK_END_TOKEN, AliasLinkCloseTag)
                .Replace(WikiPageData.ALIAS_LINK_DELIMITER, AliasLinkDelimiterTag);
        }
    }
}
