using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Text))]
public class ScoreDisplayer : MonoBehaviour {

	static Dictionary<PlayerID, ScoreDisplayer> InstancesByPlayer = new Dictionary<PlayerID, ScoreDisplayer>();

	public PlayerID Player;
	int score;
	Text scoreText;

	void Awake () {
		Init();
	}

	void OnDestroy () {
		Cleanup();
	}

	void Init () {
		if (InstancesByPlayer.ContainsKey(Player)) {
			Debug.Log("An instance of ScoreDisplayer already exists for " + Player.ToString());
			Destroy(gameObject);
		} else {
			InstancesByPlayer.Add(Player, this);
			scoreText = GetComponent<Text>();
		}
	}

	void Cleanup () {
		InstancesByPlayer.Remove(Player);
	}

	void UpdateScore (int delta) {
		score += delta;
		scoreText.text = score.ToString();
	}

	public static void ModifyScore (PlayerID player, int delta) {
		ScoreDisplayer score;
		if (InstancesByPlayer.TryGetValue(player, out score)) {
			score.UpdateScore(delta);
		}
	}
}
