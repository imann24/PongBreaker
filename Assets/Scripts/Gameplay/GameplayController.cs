/*
 * Author: Isaiah Mann
 * Description: Handles gameplay logic
 */

using UnityEngine;

public class GameplayController : SingletonBehaviour<GameplayController>
{
	[SerializeField]
	GameObject leftPaddle;
	[SerializeField]
	GameObject rightPaddle;
	[SerializeField]
	PuckController puck;

	StateController state;
	Game game;

	protected override void Start()
	{
		base.Start();
		state = StateController.Instance;
		initialize(state.CurrentGame);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if(game != null)
		{
			unsubscribeEvents(game);
		}
		resumeTime();
	}

	void initialize(Game game)
	{
		this.game = game;
		subscribeEvents(game);
		initializeGameType(game);
		game.Play();
	}

	void initializeGameType(Game game)
	{
		switch(game.Type)
		{
			case GameType.HumanVsAI:
				initializePlayerVsAIGame();
				break;
			case GameType.HumanVsHuman:
				initializePlayerVsPlayerGame();
				break;
		}
	}

	void subscribeEvents(Game game)
	{
		game.OnStart.Subscribe(resume);
		game.OnPause.Subscribe(pause);
		game.OnResume.Subscribe(resume);
		game.OnEnd.Subscribe(endGame);
	}

	void unsubscribeEvents(Game game)
	{
		game.OnStart.Unsubscribe(resume);
		game.OnPause.Unsubscribe(pause);
		game.OnResume.Unsubscribe(resume);
		game.OnEnd.Unsubscribe(endGame);
	}

	void pause()
	{
		pauseTime();
	}

	void resume()
	{
		resumeTime();
	}

	void endGame()
	{
		pauseTime();
	}
		
	void initializePlayerVsAIGame()
	{
		initializePlayerPaddle(leftPaddle, PaddlePosition.Left);
		initializeAIPaddle(rightPaddle, PaddlePosition.Right);
	}

	void initializePlayerVsPlayerGame()
	{
		initializePlayerPaddle(leftPaddle, PaddlePosition.Left);
		initializePlayerPaddle(rightPaddle, PaddlePosition.Right);
	}

	void initializeAIPaddle(GameObject paddle, PaddlePosition position)
	{
		paddle.AddComponent<AIPaddleController>().Initialize(puck, position);
	}

	void initializePlayerPaddle(GameObject paddle, PaddlePosition position)
	{
		paddle.AddComponent<PlayerPaddleController>().Initialize(puck, position);
	}
}
