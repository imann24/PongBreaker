using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class CountDownController : SingletonBehaviour<CountDownController> {
	[SerializeField]
	int MaxTextSize = 200;
	[SerializeField]
	int MinTextSize = 75;

	Text countText;

	// Use this for initialization
	protected override void Awake()
	{
		base.Awake();
		if(isSingleton)
		{
			Init();
		}
	}

	protected override void Start()
	{
		base.Start();
		Game game = StateController.Instance.CurrentGame;
		game.OnStart.Subscribe(
			delegate
			{
				gameObject.SetActive(true);
				CountDownFrom();
			}
		);
		game.OnEnd.Subscribe(delegate{gameObject.SetActive(false);});
	}

	void Init()
	{
		countText = GetComponent<Text>();
		CountDownFrom();
		Subscribe();
	}

	protected override void OnDestroy()
	{
		if(isSingleton) 
		{
			Unsubscribe();
		}
		base.OnDestroy();
	}

	void Subscribe()
	{
		EventControler.OnNamedEvent += HandleNamedEvent;	
	}

	void Unsubscribe() 
	{
		EventControler.OnNamedEvent -= HandleNamedEvent;
	}

	void HandleNamedEvent(string eventName) 
	{
		if(eventName == EventList.GOAL && gameObject.activeInHierarchy) 
		{
			CountDownFrom();
		}
	}

	public void CountDownFrom(int startingNumber = (int) Global.PUCK_RESPAWN_TIME)
	{
		StartCoroutine(CountDown(startingNumber));
	}

	IEnumerator CountDown(int startingNumber) 
	{
		int number = startingNumber;
		float shrinkTime = 1.0f;
		countText.enabled = true;
		while(number > 0) 
		{
			countText.text = number.ToString();
			StartCoroutine(Shrink(shrinkTime));
			yield return new WaitForSeconds(shrinkTime);
			number--;
		}
		countText.enabled = false;
	}

	IEnumerator Shrink(float time = 1.0f) 
	{
		float timer = 0;
		while(timer <= time)
		{
			timer += Time.deltaTime;
			countText.fontSize = (int) Mathf.Lerp(MaxTextSize, MinTextSize, timer/time);
			yield return new WaitForEndOfFrame();
		}
	}
}
