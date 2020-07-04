using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace WitchOS
{
    public class ScrollRectNoDrag : ScrollRect
    {
        public override void OnBeginDrag (PointerEventData eventData) { }
        public override void OnDrag (PointerEventData eventData) { }
        public override void OnEndDrag (PointerEventData eventData) { }
    }
}
