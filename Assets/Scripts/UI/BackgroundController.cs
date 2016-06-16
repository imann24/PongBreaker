﻿using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
	public Color StandardColor;
	public Color ScoreColor;

	Camera myCamera;

	// Use this for initialization
	void Awake () {
		Init();
	}

	void Init () {
		Subscribe();
		myCamera = GetComponent<Camera>();
	}

	void OnDestroy () {
		Unsubscribe();
	}

	void HandleNamedEvent (string namedEvent) {
		if (namedEvent == EventList.GOAL) {
			StartCoroutine(FlashColor(ScoreColor));
		}
	}

	void Subscribe () {
		EventControler.OnNamedEvent += HandleNamedEvent;
	}

	void Unsubscribe () {
		EventControler.OnNamedEvent -= HandleNamedEvent;
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
