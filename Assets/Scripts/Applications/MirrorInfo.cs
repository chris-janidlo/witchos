using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MirrorInfo : MonoBehaviour
{
	public Sprite IntactSprite, DepletedSprite;
	public List<Sprite> BrokenSprites;
	public float BrokenAnimationFPS;

	public Image Icon;
	public TextMeshProUGUI StateLabel;

	public Button BreakButton;
	public TextMeshProUGUI BreakButtonLabel;

	MirrorState.Mirror mirror;

	int frameTicker;
	public float frameTimer;

	void Update ()
	{
		if (mirror == null) return;

		BreakButton.interactable = mirror.State == MirrorState.State.Intact;
		BreakButtonLabel.text = mirror.State == MirrorState.State.Intact ? "BREAK" : "";

		int time;

		switch (mirror.State)
		{
			case MirrorState.State.Intact:
				StateLabel.text = "Intact";
				Icon.sprite = IntactSprite;
				return;

			case MirrorState.State.Broken:
				time = (int) mirror.Timer;
				StateLabel.text = $"Broken {time} minute{(time != 1 ? "s" : "")} ago";

				Icon.sprite = BrokenSprites[frameTicker];

				frameTimer -= Time.deltaTime;
				if (frameTimer <= 0)
				{
					frameTicker = (frameTicker + 1) % BrokenSprites.Count;
					frameTimer = 1 / BrokenAnimationFPS;
				}

				return;

			case MirrorState.State.Depleted:
				time = (int) mirror.Timer;
				StateLabel.text = $"Depleted. {time} minute{(time != 1 ? "s" : "")} until repaired";

				Icon.sprite = DepletedSprite;
				return;
		}
	}

	public void SetMirrorState (MirrorState.Mirror mirror)
	{
		this.mirror = mirror;

		BreakButton.onClick.AddListener(mirror.Break);
	}
}
