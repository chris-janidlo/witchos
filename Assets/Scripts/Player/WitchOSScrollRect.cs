using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace WitchOS
{
    public class WitchOSScrollRect : ScrollRect
    {
        protected override void LateUpdate ()
        {
            base.LateUpdate();
            ensurePixelPerfectScroll();
        }

        // override drag events with empty bodies to disable mouse drag
        public override void OnBeginDrag (PointerEventData eventData) { }
        public override void OnDrag (PointerEventData eventData) { }
        public override void OnEndDrag (PointerEventData eventData) { }

        void ensurePixelPerfectScroll ()
        {
            // from https://stackoverflow.com/a/64235663/5931898 (which is me >:) )
            float normalizedPixel = 1 / (content.rect.height - viewport.rect.height);
            verticalNormalizedPosition = Mathf.Ceil(verticalNormalizedPosition / normalizedPixel) * normalizedPixel;
        }
    }
}
