/*
 * Author: Isaiah Mann
 * Description:
 */

public class StateController : SingletonBehaviour<StateController>
{
	public Game CurrentGame
	{
		get
		{
			if(_currentGame == null)
			{
				_currentGame = getDefaultGame();
			}
			return _currentGame;
		}
	}

	Game _currentGame;
		
	Game getDefaultGame()
	{
		return new Game(default(GameType));
	}
		
	SceneController scene;

	public void LaunchGame(Game game)
	{
		this._currentGame = game;
		scene.LoadScene(GameScene.Game);
	}

	protected override void Start()
	{
		base.Start();
		this.scene = SceneController.Instance;
	}

	protected override void OnSceneLoad(int sceneIndex)
	{
		base.OnSceneLoad(sceneIndex);
		if((GameScene) sceneIndex == GameScene.Game)
		{

		}
	}
}
