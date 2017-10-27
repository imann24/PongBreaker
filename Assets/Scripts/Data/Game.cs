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

	public GameType Type
	{
		get;
		private set;
	}

	public int ScoreToWin
	{
		get;
		private set;
	}

	public GameState State
	{
		get;
		private set;
	}

	public Game(GameType type, int scoreToWin)
	{
		this.Type = type;
		this.ScoreToWin = scoreToWin;
		this.State = GameState.Inactive;
		OnStart = new DelegateAction(this);
		OnResume = new DelegateAction(this);
		OnPause = new DelegateAction(this);
		OnEnd = new DelegateAction(this);
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
}
