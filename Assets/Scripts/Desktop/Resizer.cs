using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using crass;

namespace WitchOS
{
public class Resizer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public const int EDGE_FUDGE = 2; // if the mouse is within this many pixels from the edge of the window, it's allowed to drag
	public const int CORNER_FUDGE = 4; // how far away the mouse can be from the actual literal location of the corner for it to still be considered a corner drag

	public bool Resizing { get; private set; }
	public bool HoveringOverResizeZone { get; private set; }

    public Vector2 MinSize = new Vector2 (100, 100);
	public Vector2 MaxSize = new Vector2 (480, 250); // default is size of window area

	public RectTransform TargetTransform;
	public Window Window;

	RectTransform targetParent => TargetTransform.parent as RectTransform;

	Vector2 startingMousePositionInParent; // we track the position of the mouse relative to the parent for ease in clamping to the parent
	Vector2 startingSizeDelta;

	Vector2 dragDirection; // how the change in size should be applied, relative from the pivot

	void Start ()
	{
		if (TargetTransform == null)
		{
			throw new InvalidOperationException("TargetTransform cannot be null");
		}
	}

	void Update ()
	{
		if (!Window.Focused || Resizing) return;

		Vector2 mousePos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(TargetTransform, Input.mousePosition, CameraCache.Main, out mousePos);

		if
		(
			mousePos.x < TargetTransform.rect.xMin - EDGE_FUDGE || mousePos.x > TargetTransform.rect.xMax + EDGE_FUDGE ||
			mousePos.y < TargetTransform.rect.yMin - EDGE_FUDGE || mousePos.y > TargetTransform.rect.yMax + EDGE_FUDGE
		)
		{
			CursorManager.Instance.CursorState = CursorState.Normal;
			return;
		}

		Vector2 centerToMouseDirection = mousePos - TargetTransform.rect.center;

		Vector2 closestCorner = new Vector2
		(
			centerToMouseDirection.x < 0 ? TargetTransform.rect.xMin : TargetTransform.rect.xMax,
			centerToMouseDirection.y < 0 ? TargetTransform.rect.yMin : TargetTransform.rect.yMax
		);

		// really just a bounds check - sees if either the pointer x or the pointer y is close enough to the corresponding vector-component of the closest corner
		HoveringOverResizeZone =
			Mathf.Abs(mousePos.x - closestCorner.x) <= EDGE_FUDGE ||
			Mathf.Abs(mousePos.y - closestCorner.y) <= EDGE_FUDGE;

		dragDirection = new Vector2
		(
			Math.Sign(centerToMouseDirection.x),
			Math.Sign(centerToMouseDirection.y)
		);

		if (Mathf.Abs(mousePos.x - closestCorner.x) > CORNER_FUDGE) dragDirection.x = 0;
		if (Mathf.Abs(mousePos.y - closestCorner.y) > CORNER_FUDGE) dragDirection.y = 0;

		CursorState newState;

		if (!HoveringOverResizeZone)
		{
			newState = CursorState.Normal;
		}
		else
		{
			if (dragDirection == Vector2.left || dragDirection == Vector2.right)
			{
				newState = CursorState.HorizontalResize;
			}
			else if (dragDirection == Vector2.up || dragDirection == Vector2.down)
			{
				newState = CursorState.VerticalResize;
			}
			else if (dragDirection == -Vector2.one || dragDirection == Vector2.one)
			{
				newState = CursorState.DiagonalFromSoutheastResize;
			}
			else
			{
				newState = CursorState.DiagonalFromSouthwestResize;
			}
		}

		CursorManager.Instance.CursorState = newState;
	}

	public void OnPointerDown (PointerEventData data)
    {
		if (!Window.Focused || !HoveringOverResizeZone) return;

		Resizing = true;

		// set pivot so that dragging happens like in modern OSes: the panel expands in the direction you drag it, but the corner or edge exactly opposite your cursor stays anchored
		// we find the right pivot by negating the drag direction then scaling it from -1,1 to 0,1
		TargetTransform.SetPivot((-dragDirection + Vector2.one) / 2);

		startingSizeDelta = TargetTransform.sizeDelta;

		RectTransformUtility.ScreenPointToLocalPointInRectangle(targetParent, data.position, data.pressEventCamera, out startingMousePositionInParent);
	}

	public void OnPointerUp (PointerEventData data)
	{
		Resizing = false;
	}

	public void OnDrag (PointerEventData data)
    {
		if (!Window.Focused || !Resizing) return;

		Vector2 currentMousePositionInParent;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(targetParent, data.position, data.pressEventCamera, out currentMousePositionInParent);

		// clamp prevents the recttransform from expanding in the opposite direction when it hits a parent corner/edge
		currentMousePositionInParent = new Vector2
		(
			Mathf.Clamp(currentMousePositionInParent.x, targetParent.rect.xMin, targetParent.rect.xMax),
			Mathf.Clamp(currentMousePositionInParent.y, targetParent.rect.yMin, targetParent.rect.yMax)
		);

		Vector2 dragDelta = currentMousePositionInParent - startingMousePositionInParent;

		// scale dragDelta by dragDirection to get 8-way drag behavior
		Vector2 newSizeDelta = startingSizeDelta + Vector2.Scale(dragDelta, dragDirection);

		// rounds to nearest even integer since odd size deltas lead to ugly half-pixel values
		newSizeDelta = new Vector2Int
        (
			Mathf.RoundToInt(Mathf.Clamp(newSizeDelta.x, MinSize.x, MaxSize.x) / 2) * 2,
			Mathf.RoundToInt(Mathf.Clamp(newSizeDelta.y, MinSize.y, MaxSize.y) / 2) * 2
		);

		TargetTransform.sizeDelta = newSizeDelta;
    }
}
}
