/**
 * Author: Isaiah Mann
 * Description: Handles score logic
 */

using UnityEngine;

public class ScoreController : MonoBehaviourExtended 
{
	Game game;

	public void ResetScore()
	{
		ScoreDisplayer.ResetScores();
	}

	protected override void Start()
	{
		base.Start();
		game = StateController.Instance.CurrentGame;
		game.OnRestart.Subscribe(ResetScore);
	}

	protected override void HandleNamedEvent(string eventName)
	{
		base.HandleNamedEvent(eventName);
		PaddlePosition scoringPlayer = PlayerUtil.GetOpponent(PlayerUtil.GetPlayerFromGoalTag(eventName));
		if(scoringPlayer != PaddlePosition.None) 
		{
			if(game == null)
			{
				Debug.LogErrorFormat("Game is null for {0} instance", GetType());
				return;
			}
			updateScore(game, scoringPlayer);
			EventController.Event(EventList.GOAL);
		}
	}

	void updateScore(Game game, PaddlePosition scoringPlayer)
	{
		int score = game.ScoreGoal(scoringPlayer);
		ScoreDisplayer.ModifyScore(scoringPlayer, score);
	}
}
