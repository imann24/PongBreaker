using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class CountDownController : MonoBehaviour {
	public static CountDownController Instance;

	public int MaxTextSize = 200;
	public int MinTextSize = 75;


	Text countText;

	bool hasInit;

	// Use this for initialization
	void Awake () {
		if (SingletonUtil.TryInit(ref Instance, this, gameObject)) {
			Init();
			hasInit = true;
		}
	}

	void Init () {
		countText = GetComponent<Text>();
		CountDownFrom();
		Subscribe();
	}

	void OnDestroy () {
		if (hasInit) {
			Unsubscribe();
		}
	}

	void Subscribe () {
		EventControler.OnNamedEvent += HandleNamedEvent;	
	}

	void Unsubscribe () {
		EventControler.OnNamedEvent -= HandleNamedEvent;
	}

	void HandleNamedEvent (string eventName) {
		if (eventName == EventList.GOAL) {
			CountDownFrom();
		}
	}

	public void CountDownFrom (int startingNumber = (int) Global.PUCK_RESPAWN_TIME) {
		StartCoroutine(CountDown(startingNumber));
	}

	IEnumerator CountDown (int startingNumber) {
		int number = startingNumber;
		float shrinkTime = 1.0f;
		countText.enabled = true;
		while (number > 0) {
			countText.text = number.ToString();
			StartCoroutine(Shrink(shrinkTime));
			yield return new WaitForSeconds(shrinkTime);
			number--;
		}
		countText.enabled = false;
	}

	IEnumerator Shrink (float time = 1.0f) {
		float timer = 0;
		while (timer <= time) {
			timer += Time.deltaTime;
			countText.fontSize = (int) Mathf.Lerp(MaxTextSize, MinTextSize, timer/time);
			yield return new WaitForEndOfFrame();
		}
	}
}
