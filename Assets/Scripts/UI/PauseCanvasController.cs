/*
 * Author: Isaiah Mann
 * Description:
 */

using UnityEngine;
using UnityEngine.UI;

public class PauseCanvasController : UIController
{
	[SerializeField]
	UIButton resumeButton;
	[SerializeField]
	UIButton quitButton;

	CanvasGroup canvas;
	StateController state;
	Game game;

	protected override void Awake()
	{
		base.Awake();
		canvas = GetComponent<CanvasGroup>();
	}

	protected override void Start()
	{
		base.Start();
		state = StateController.Instance;
		game = state.CurrentGame;
		setupButtons();
		subscribeCanvasEvents();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		unsubscribeCanvasEvents();
	}

	void setupButtons()
	{
		resumeButton.AddAction(game.Resume);
		quitButton.AddAction(state.LoadMainMenu);
	}

	void subscribeCanvasEvents()
	{
		game.OnResume.Subscribe(hideCanvas);
		game.OnPause.Subscribe(showCanvas);
	}

	void unsubscribeCanvasEvents()
	{
		game.OnResume.Unsubscribe(hideCanvas);
		game.OnPause.Unsubscribe(showCanvas);
	}

	void hideCanvas()
	{
		toggleCanvasGroup(canvas, false);
	}

	void showCanvas()
	{
		toggleCanvasGroup(canvas, true);
	}
}
