using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ClampToParent : MonoBehaviour
{
    void Update ()
    {
        var rectTransform = GetComponent<RectTransform>();
        var parentRect = rectTransform.parent as RectTransform;

        Vector3 minPosition = parentRect.rect.min - rectTransform.rect.min;
        Vector3 maxPosition = parentRect.rect.max - rectTransform.rect.max;

        rectTransform.localPosition = new Vector3
        (
            Mathf.Clamp(rectTransform.localPosition.x, minPosition.x, maxPosition.x),
            Mathf.Clamp(rectTransform.localPosition.y, minPosition.y, maxPosition.y),
            rectTransform.localPosition.z
        );
    }
}
