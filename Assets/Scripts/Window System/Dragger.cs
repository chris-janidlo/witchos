using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using crass;

public class Dragger : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RectTransform TransformToMove;

    bool dragging;
    Vector3 offset;

	void Start ()
    {
        if (!transform.IsChildOf(TransformToMove))
        {
            Debug.LogWarning("careful, no parent child relationship");
        }
    }

    void Update ()
    {
        if (dragging)
        {
            TransformToMove.localPosition = new Vector3
            (
                Mathf.RoundToInt(TransformToMove.localPosition.x),
                Mathf.RoundToInt(TransformToMove.localPosition.y),
                Mathf.RoundToInt(TransformToMove.localPosition.z)
            );
        }
    }

	public void OnBeginDrag (PointerEventData eventData)
	{
        Vector3 pos = CameraCache.Main.ScreenToWorldPoint(eventData.position);
        pos.z = 0;
        offset = TransformToMove.position - pos;

        dragging = true;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
        dragging = false;
	}

	public void OnDrag (PointerEventData eventData)
	{
        Vector3 pos = CameraCache.Main.ScreenToWorldPoint(eventData.position);
        pos.z = 0;
        TransformToMove.position = offset + pos;
	}
}
