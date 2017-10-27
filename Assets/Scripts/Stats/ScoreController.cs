/**
 * Author: Isaiah Mann
 * Description: Handles score logic
 */

using UnityEngine;

public class ScoreController : MonoBehaviourExtended 
{
	Game game;

	protected override void Awake()
	{
		base.Awake();
		Init();
	}
		
	protected override void Start()
	{
		base.Start();
		game = StateController.Instance.CurrentGame;
	}

	void OnDestroy () {
		Unsubscribe();
	}

	void Init () {
		Subscribe();
	}

	void Subscribe () {
		EventControler.OnNamedEvent += HandleNamedEvent;
	}

	void Unsubscribe () {
		EventControler.OnNamedEvent -= HandleNamedEvent;
	}

	void HandleNamedEvent (string eventName) {
		PaddlePosition scoringPlayer = PlayerUtil.GetOpponent(PlayerUtil.GetPlayerFromGoalTag(eventName));

		if (scoringPlayer != PaddlePosition.None) {
			if(game == null)
			{
				Debug.LogErrorFormat("Game is null for {0} instance", GetType());
				return;
			}
			int score = game.ScoreGoal(scoringPlayer);
			ScoreDisplayer.ModifyScore(scoringPlayer, score);
			EventControler.Event(EventList.GOAL);
		}
	}
}
