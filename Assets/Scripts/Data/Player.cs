/*
 * Author: Isaiah Mann
 * Description:
 */

public class Player
{
	public DelegateAction OnPlayerWin
	{
		get;
		private set;
	}

	public int Score
	{
		get;
		private set;
	}
		
	public PlayerType Type
	{
		get;
		private set;
	}

	public int Index
	{
		get;
		private set;
	}

	Tuning tuning;

	public Player(PlayerType type, Tuning tuning, int index)
	{
		this.OnPlayerWin = new DelegateAction(this);
		this.Score = 0;
		Type = type;
		this.tuning = tuning;
		this.Index = index;
	}

	public int ScoreGoal()
	{
		Score += tuning.BasePointsFromGoal;
		if(Score >= tuning.ScoreToWin)
		{
			OnPlayerWin.Call(this);
		}
		return Score;
	}

	public void ResetScore()
	{
		Score = 0;
	}
}
