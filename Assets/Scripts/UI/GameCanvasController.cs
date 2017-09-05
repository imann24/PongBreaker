/*
 * Author: Isaiah Mann
 * Description: UI for the game
 */

using UnityEngine;

public class GameCanvasController : UIController
{
	[SerializeField]
	UIButton pauseButton;

	[SerializeField]
	Sprite pauseIcon;
	[SerializeField]
	Sprite resumeIcon;

	StateController state;
	Game currentGame;

	protected override void Start()
	{
		base.Start();
		state = StateController.Instance;
		currentGame = state.CurrentGame;
		setupPauseButton();
	}

	void setupPauseButton()
	{
		pauseButton.AddAction(togglePause);
		currentGame.OnPause.Subscribe(setPauseButtonToResumeIcon);
		currentGame.OnResume.Subscribe(setPauseButtonToPauseIcon);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		teardownPauseButton();
	}

	void teardownPauseButton()
	{
		currentGame.OnPause.Unsubscribe(setPauseButtonToResumeIcon);
		currentGame.OnResume.Unsubscribe(setPauseButtonToPauseIcon);
	}
		
	void togglePause()
	{
		switch(currentGame.State)
		{
			case GameState.InPlay:
				state.PauseGame(currentGame);
				break;
			case GameState.Paused:
				state.ResumeGame(currentGame);
				break;
		}
	}

	void setPauseButtonToResumeIcon()
	{
		pauseButton.SetImage(resumeIcon);
	}

	void setPauseButtonToPauseIcon()
	{
		pauseButton.SetImage(pauseIcon);
	}
}
