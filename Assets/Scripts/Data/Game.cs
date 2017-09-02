/*
 * Author: Isaiah Mann
 * Description: Determines the game
 */

[System.Serializable]
public class Game
{
	public GameType Type 
	{
		get;
		private set;
	}

	public Game(GameType type)
	{
		this.Type = type;
	}
}
