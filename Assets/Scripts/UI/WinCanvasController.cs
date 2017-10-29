/*
 * Author: Isaiah Mann
 * Description: Controls the display of the win screen for the game
 */

using UnityEngine;
using UnityEngine.UI;

public class WinCanvasController : MonoBehaviourExtended
{
	[SerializeField]
	string winTextFormat = "Player {0} Wins!";

	[SerializeField]
	Text winText;

	[SerializeField]
	GameObject uiRect;

	[SerializeField]
	UIButton menuButton;
	[SerializeField]
	UIButton playAgainButton;

	protected override void Start()
	{
		base.Start();
		StateController state = StateController.Instance;
		createWinHandlers(state.CurrentGame);
		setupButtons(state);
	}

	void createWinHandlers(Game game)
	{
		foreach(Player player in game.Players)
		{
			Player currentPlayer = player;
			player.OnPlayerWin.Subscribe(delegate 
				{
					uiRect.SetActive(true);
					winText.text = string.Format(winTextFormat, currentPlayer.Index);
				}
			);
		}
	}

	void setupButtons(StateController state)
	{
		menuButton.AddAction(state.LoadMainMenu);
		playAgainButton.AddAction(
			delegate 
			{
				uiRect.SetActive(false);
				state.RestartGame(state.CurrentGame);
			}
		);
	}
}
