using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreController : MonoBehaviour {

	Dictionary <PaddlePosition, int> scores = new Dictionary<PaddlePosition, int>();

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
		scores.Add(PaddlePosition.Left, 0);
		scores.Add(PaddlePosition.Right, 0);
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
			int score = Score(scoringPlayer);
			scores[scoringPlayer] += score;
			ScoreDisplayer.ModifyScore(scoringPlayer, score);
			EventControler.Event(EventList.GOAL);
		}
	}

	int Score (PaddlePosition scoringPlayer) {
		return GetMultiplier(scoringPlayer) * Global.BASE_GOAL_SCORE;
	}

	// TODO: Add more
	int GetMultiplier (PaddlePosition scoringPlayer) {
		return 1;
	}

}
