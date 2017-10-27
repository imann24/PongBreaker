/*
 * Author: Isaiah Mann
 * Description:
 */

using UnityEngine;

[System.Serializable]
public class Tuning
{
	public static Tuning Get
	{
		get
		{
			if(_instance == null)
			{
				_instance = initialize();
			}
			return _instance;
		}
	}

	static Tuning _instance;

	static Tuning initialize()
	{
		TextAsset json = Resources.Load<TextAsset>("JSON/Tuning");
		return JsonUtility.FromJson<Tuning>(json.text);
	}

	public int ScoreToWin 
	{
		get 
		{
			return scoreToWin;
		}
	}

	public int BasePointsFromGoal
	{
		get
		{
			return basePointsFromGoal;
		}
	}

	[SerializeField]
	int scoreToWin;
	[SerializeField]
	int basePointsFromGoal;
}
