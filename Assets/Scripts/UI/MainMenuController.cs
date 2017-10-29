/*
 * Author: Isaiah Mann
 * Description: Controls behaviour of the main menu
 */

using UnityEngine;

public class MainMenuController : MonoBehaviourExtended
{
	Tuning tuning;

	[SerializeField]
	UIButton playGameVsAIButton, playGameVsPlayerButton;

	StateController state;

	protected override void Awake()
	{
		base.Awake();
		tuning = Tuning.Get;
	}

	protected override void Start()
	{
		base.Start();
		state = StateController.Instance;
		playGameVsAIButton.AddAction(launchVsAIGame);
		playGameVsPlayerButton.AddAction(launchVsPlayerGame);
	}

	void launchVsAIGame()
	{
		state.LaunchGame(new Game(GameType.HumanVsAI, tuning));	
	}

	void launchVsPlayerGame()
	{
		state.LaunchGame(new Game(GameType.HumanVsHuman, tuning));
	}
}
