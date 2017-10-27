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

	Tuning tuning;

	public Player(PlayerType type, Tuning tuning)
	{
		this.OnPlayerWin = new DelegateAction(this);
		this.Score = 0;
		Type = type;
		this.tuning = tuning;
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
}
