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

	protected override void Start()
	{
		base.Start();
		state = StateController.Instance;
		initialize(state.CurrentGame);
	}

	void initialize(Game game)
	{
		switch(game.Type)
		{
			case GameType.PlayerVsAI:
				initializePlayerVsAIGame();
				break;
			case GameType.PlayerVsPlayer:
				initializePlayerVsPlayerGame();
				break;
		}
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
