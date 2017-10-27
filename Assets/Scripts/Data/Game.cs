/*
 * Author: Isaiah Mann
 * Description: Determines the game
 */

using System;

[System.Serializable]
public class Game
{
	public Boolean Running
	{
		get
		{
			return State == GameState.InPlay;
		}
	}

	public DelegateAction OnStart
	{
		get;
		private set;
	}

	public DelegateAction OnResume
	{
		get;
		private set;
	}

	public DelegateAction OnPause
	{
		get;
		private set;
	}

	public DelegateAction OnEnd
	{
		get;
		private set;
	}

	public Player LeftPlayer
	{
		get;
		private set;
	}

	public Player RightPlayer
	{
		get;
		private set;
	}

	public Player this[PaddlePosition paddle]
	{
		get
		{
			switch(paddle)
			{
				case PaddlePosition.Left:
					return LeftPlayer;
				case PaddlePosition.Right:
					return RightPlayer;
				default:
					return null;
			}
		}
	}

	public GameType Type
	{
		get;
		private set;
	}

	public GameState State
	{
		get;
		private set;
	}

	public Game(GameType type, Tuning tuning)
	{
		this.Type = type;
		this.State = GameState.Inactive;
		OnStart = new DelegateAction(this);
		OnResume = new DelegateAction(this);
		OnPause = new DelegateAction(this);
		OnEnd = new DelegateAction(this);
		LeftPlayer = new Player(PlayerType.Human, tuning);
		RightPlayer = new Player(type == GameType.HumanVsHuman ? PlayerType.Human : PlayerType.AI, tuning);
	}

	public void Play()
	{
		this.State = GameState.InPlay;
		OnStart.Call(this);
	}

	public void Resume()
	{
		this.State = GameState.InPlay;
		OnResume.Call(this);
	}

	public void Pause()
	{
		this.State = GameState.Paused;
		OnPause.Call(this);
	}

	public void End()
	{
		this.State = GameState.Paused;
		OnEnd.Call(this);
	}

	public int ScoreGoal(PaddlePosition paddle)
	{
		Player player = this[paddle];
		if(player == null)
		{
			return default(int);
		}
		else
		{
			return player.ScoreGoal();
		}
	}
}
