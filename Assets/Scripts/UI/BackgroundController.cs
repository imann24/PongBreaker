using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviourExtended
{
	public Color StandardColor;
	public Color ScoreColor;
	public Color BreakColor;

	Camera myCamera;

	IEnumerator flashCoroutine;

	// Use this for initialization
	protected override void Awake()
	{
		base.Awake();
		Init();
	}

	void Init() 
	{
		myCamera = GetComponent<Camera>();
	}

	protected override  void HandleNamedEvent (string namedEvent) 
	{
		base.HandleNamedEvent(namedEvent);
		if(namedEvent == EventList.GOAL) 
		{
			CallFlashCoroutine(ScoreColor);
		}
		else if(namedEvent == EventList.BRICK_BROKEN) 
		{
			CallFlashCoroutine(BreakColor);
		}
	}

	void CallFlashCoroutine (Color color) {
		if (flashCoroutine != null) {
			StopCoroutine(flashCoroutine);
		}

		flashCoroutine = FlashColor(color);
		StartCoroutine(flashCoroutine);
	}

	IEnumerator FlashColor (Color color, float lerpTime = 0.35f, float stayTime = 0.5f) {
		float timer = 0;

		while (timer <= lerpTime) {
			timer += Time.deltaTime;
			myCamera.backgroundColor = Color.Lerp(StandardColor, color, timer/lerpTime);
			yield return new WaitForEndOfFrame();
		}

		myCamera.backgroundColor = color;

		yield return new WaitForSeconds(stayTime);

		timer = 0;

		while (timer <= lerpTime) {
			timer += Time.deltaTime;
			myCamera.backgroundColor = Color.Lerp(color, StandardColor, timer/lerpTime);
			yield return new WaitForEndOfFrame();
		}

		myCamera.backgroundColor = StandardColor;
	}
}
