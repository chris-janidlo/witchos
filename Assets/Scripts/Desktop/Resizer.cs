using System;
using UnityEngine;
using UnityEngine.EventSystems;
using crass;

public class Resizer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public const int EDGE_FUDGE = 4; // if the mouse is within this many pixels from the edge of the window, it's allowed to drag
	public const int CORNER_FUDGE = 6; // how far away the mouse can be from the actual literal location of the corner for it to still be considered a corner drag

	public bool Resizing { get; private set; }

    public Vector2 MinSize = new Vector2 (100, 100);
	public Vector2 MaxSize = new Vector2 (480, 250); // default is size of window area

	public RectTransform TargetTransform;

	Vector2 startingMouseLocalPosition;
	Vector2 startingSizeDelta;

	Vector2 dragDirection; // how the change in size should be applied, relative from the pivot

	void Start ()
	{
		if (TargetTransform == null)
		{
			throw new InvalidOperationException("TargetTransform cannot be null");
		}
	}

	public void OnPointerDown (PointerEventData data)
    {
		// DETERMINE RESIZE STATE

		Vector2 mousePos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(TargetTransform, data.position, data.pressEventCamera, out mousePos);

		Vector2 centerToMouseDirection = mousePos - TargetTransform.rect.center;

		Vector2 closestCorner = new Vector2
		(
			centerToMouseDirection.x < 0 ? TargetTransform.rect.xMin : TargetTransform.rect.xMax,
			centerToMouseDirection.y < 0 ? TargetTransform.rect.yMin : TargetTransform.rect.yMax
		);

		// really just a bounds check - sees if either the pointer x or the pointer y is close enough to the corresponding component of the closest corner
		Resizing =
			Mathf.Abs(mousePos.x - closestCorner.x) <= EDGE_FUDGE ||
			Mathf.Abs(mousePos.y - closestCorner.y) <= EDGE_FUDGE;

		if (!Resizing) return;

		// BEGIN RESIZE STATE

		dragDirection = new Vector2
		(
			Math.Sign(centerToMouseDirection.x),
			Math.Sign(centerToMouseDirection.y)
		);

		if (Mathf.Abs(mousePos.x - closestCorner.x) > CORNER_FUDGE) dragDirection.x = 0;
		if (Mathf.Abs(mousePos.y - closestCorner.y) > CORNER_FUDGE) dragDirection.y = 0;

		// set pivot so that dragging happens like in modern OSes: the panel expands in the direction you drag it, but the corner or edge exactly opposite your cursor stays anchored
		// essentially, this flips the drag direction then scales it from -1,1 to 0,1
		TargetTransform.SetPivot((-dragDirection + Vector2.one) / 2);

		startingSizeDelta = TargetTransform.sizeDelta;

		// can't use the mousePos from above because of the pivot change
		RectTransformUtility.ScreenPointToLocalPointInRectangle(TargetTransform, data.position, data.pressEventCamera, out startingMouseLocalPosition);
	}

	public void OnPointerUp (PointerEventData data)
	{
		Resizing = false;
	}

	public void OnDrag (PointerEventData data)
    {
		if (!Resizing) return;

		Vector2 mousePos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(TargetTransform, data.position, data.pressEventCamera, out mousePos);

		Vector2 dragDelta = mousePos - startingMouseLocalPosition;

		// scale dragDelta by dragDirection to get 8-way drag behavior
		Vector2 newSizeDelta = startingSizeDelta + Vector2.Scale(dragDelta, dragDirection);

		// rounds to nearest even integer since odd size deltas lead to half-pixel uglies
		newSizeDelta = new Vector2Int
        (
			Mathf.RoundToInt(Mathf.Clamp(newSizeDelta.x, MinSize.x, MaxSize.x) / 2) * 2,
			Mathf.RoundToInt(Mathf.Clamp(newSizeDelta.y, MinSize.y, MaxSize.y) / 2) * 2
		);

		TargetTransform.sizeDelta = newSizeDelta;
    }
}
