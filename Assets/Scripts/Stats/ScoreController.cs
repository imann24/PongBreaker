using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreController : MonoBehaviour {

	Dictionary <PlayerID, int> scores = new Dictionary<PlayerID, int>();

	void Awake () {
		Init();
	}
		
	void OnDestroy () {
		Unsubscribe();
	}

	void Init () {
		Subscribe();
		InitScores();
	}

	void InitScores () {
		scores.Add(PlayerID.Left, 0);
		scores.Add(PlayerID.Right, 0);
	}

	void Subscribe () {
		EventControler.OnNamedEvent += HandleNamedEvent;
	}

	void Unsubscribe () {
		EventControler.OnNamedEvent -= HandleNamedEvent;
	}

	void HandleNamedEvent (string eventName) {
		PlayerID scoringPlayer = PlayerUtil.GetOpponent(PlayerUtil.GetPlayerFromGoalTag(eventName));

		if (scoringPlayer != PlayerID.None) {
			int score = Score(scoringPlayer);
			scores[scoringPlayer] += score;
			ScoreDisplayer.ModifyScore(scoringPlayer, score);
			EventControler.Event(EventList.GOAL);
		}
	}

	int Score (PlayerID scoringPlayer) {
		return GetMultiplier(scoringPlayer) * Global.BASE_GOAL_SCORE;
	}

	// TODO: Add more
	int GetMultiplier (PlayerID scoringPlayer) {
		return 1;
	}

}
