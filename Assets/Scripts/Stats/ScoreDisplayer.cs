using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Text))]
public class ScoreDisplayer : MonoBehaviour {

	static Dictionary<PaddlePosition, ScoreDisplayer> InstancesByPlayer = new Dictionary<PaddlePosition, ScoreDisplayer>();

	public PaddlePosition Player;
	int score;
	Text scoreText;

	void Awake () {
		Init();
	}

	void OnDestroy () {
		Cleanup();
	}

	void Init() 
	{
		if(InstancesByPlayer.ContainsKey(Player)) 
		{
			Debug.Log("An instance of ScoreDisplayer already exists for " + Player.ToString());
			Destroy(gameObject);
		}
		else 
		{
			InstancesByPlayer.Add(Player, this);
			scoreText = GetComponent<Text>();
		}
	}

	void Cleanup () {
		InstancesByPlayer.Remove(Player);
	}

	void UpdateScore (int delta) {
		setScore(score + delta);
	}

	void setScore(int newValue)
	{
		score = newValue;
		scoreText.text = score.ToString();
	}
		
	public static void ModifyScore(PaddlePosition player, int newScore)
	{
		ScoreDisplayer scoreDisplay;
		if(InstancesByPlayer.TryGetValue(player, out scoreDisplay)) 
		{
			scoreDisplay.setScore(newScore);
		}
	}

	public void ResetScore()
	{
		setScore(0);
	}

	public static void ResetScores()
	{
		foreach(ScoreDisplayer score in InstancesByPlayer.Values)
		{
			score.ResetScore();
		}
	}
}
