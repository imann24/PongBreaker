/*
 * Author: Isaiah Mann
 * Description: Handles gameplay logic
 */

using System.Collections.Generic;

using UnityEngine;

public class GameplayController : SingletonBehaviour<GameplayController>
{
	public List<PaddleController> GetRegisteredPlayers
	{
		get
		{
			return paddles;
		}
	}

	[SerializeField]
	PuckController puckPrefab;
	[SerializeField]
	Transform puckParent;

	[SerializeField]
	GameObject leftPaddle;
	[SerializeField]
	GameObject rightPaddle;
	[SerializeField]
	GameObject leftGoal;
	[SerializeField]
	GameObject rightGoal;
	[SerializeField]
	PuckController puck;

	StateController state;
	PowerUpController powerUp;
	Game game;
	PaddlePosition lastPlayerHit = PaddlePosition.None;
	List<PuckController> allPucks = new List<PuckController>();
	List<PuckController> livePucks = new List<PuckController>();
	List<PaddleController> paddles = new List<PaddleController>();
    Dictionary<PhysicalObjectType, List<Vector2>> recordedPositions = new Dictionary<PhysicalObjectType, List<Vector2>>();
    Rect boardBounds = new Rect();

    protected override void Start()
	{
		base.Start();
		state = StateController.Instance;
		powerUp = PowerUpController.Instance;
		initialize(state.CurrentGame);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if(game != null)
		{
			unsubscribeEvents(game);
		}
		resumeTime();
	}

	public List<PuckController> GetLivePucks()
	{
		return new List<PuckController>(livePucks);
	}

	public void RegisterPlayer(PaddleController paddle)
	{
		paddles.Add(paddle);
	}

	public void UnregisterPlayer(PaddleController paddle)
	{
		paddles.Remove(paddle);
	}

	public void RegisterPuck(PuckController puck)
	{
		allPucks.Add(puck);
		livePucks.Add(puck);
	}

	public void DeactivatePuck(PuckController puck)
	{
		livePucks.Remove(puck);
	}

	public List<PuckController> SpawnPucks(Vector2 position, int count)
	{
		List<PuckController> pucks = getPucksToSpawn(count);
		foreach(PuckController puck in pucks)
		{
			puck.SpawnPuck(position);
		}
		return pucks;
	}

    public PaddleController GetOpponent(PaddleController paddle)
    {
        return paddles.Find((PaddleController compare) => compare != paddle);
    }

    public void RecordPosition(Vector2 position, PhysicalObjectType objectType) 
    {
        List<Vector2> positions;
        if(!recordedPositions.TryGetValue(objectType, out positions))
        {
            positions = new List<Vector2>();
            recordedPositions.Add(objectType, positions);
        }
        positions.Add(position);
        updateLogicFromRecordedPositions();
    }

    void updateLogicFromRecordedPositions()
    {
        List<Vector2> positions;
        if (recordedPositions.TryGetValue(PhysicalObjectType.Bound, out positions) && 
            positions.Count == 4) {
            float xMin = float.MaxValue;
            float yMin = float.MaxValue;
            float xMax = float.MinValue;
            float yMax = float.MinValue;
            foreach(Vector2 pos in positions)
            {
                // TODO: Finish bounds
            }
        }
    }

	List<PuckController> getPucksToSpawn(int count)
	{
		List<PuckController> spawnPucks = getSparePucks();
		if(spawnPucks.Count >= count)
		{
			return spawnPucks.GetRange(0, count);
		}
		int difference = count - spawnPucks.Count;
		for(int i = 0; i < difference; i++)
		{
			PuckController puck = Instantiate(puckPrefab, puckParent);
			allPucks.Add(puck);
			spawnPucks.Add(puck);
		}
		return spawnPucks;
	}

	List<PuckController> getSparePucks()
	{
		return allPucks.FindAll(puck => !puck.IsAlive);
	}
		
	bool puckIsAlive(PuckController puck)
	{
		return puck.IsAlive;
	}

	public void RegisterPlayerHit(PaddleController paddle)
	{
		lastPlayerHit = paddle.PaddlePosition;
	}
		
	public void HandleBrickDestroyed(BrickController brick)
	{
		if(shouldSpawnPowerUp(brick))
		{
			powerUp.SpawnPowerUp(brick, lastPlayerHit);
		}
	}

	protected override void HandleNamedEvent(string eventName)
	{
		base.HandleNamedEvent(eventName);
		if(eventName.Contains(Global.GOAL))
		{
			lastPlayerHit = PaddlePosition.None;
		}
	}

	bool shouldSpawnPowerUp(BrickController brick)
	{
		return lastPlayerHit != PaddlePosition.None && 
			brick.CanSpawnPowerUps && 
			powerUp.ShouldSpawnPowerUp();
	}
		
	void initialize(Game game)
	{
		this.game = game;
		subscribeEvents(game);
		initializeGameType(game);
		game.Play();
	}

	void initializeGameType(Game game)
	{
		switch(game.Type)
		{
			case GameType.HumanVsAI:
				initializePlayerVsAIGame();
				break;
			case GameType.HumanVsHuman:
				initializePlayerVsPlayerGame();
				break;
		}
	}

	void subscribeEvents(Game game)
	{
		game.OnStart.Subscribe(resume);
		game.OnPause.Subscribe(pause);
		game.OnResume.Subscribe(resume);
		game.OnEnd.Subscribe(endGame);
	}

	void unsubscribeEvents(Game game)
	{
		game.OnStart.Unsubscribe(resume);
		game.OnPause.Unsubscribe(pause);
		game.OnResume.Unsubscribe(resume);
		game.OnEnd.Unsubscribe(endGame);
	}

	void pause()
	{
		pauseTime();
	}

	void resume()
	{
		resumeTime();
	}

	void endGame()
	{
		pauseTime();
	}
		
	void initializePlayerVsAIGame()
	{
		initializePlayerPaddle(leftPaddle, PaddlePosition.Left);
		initializeAIPaddle(rightPaddle, PaddlePosition.Right);
	}

	void initializePlayerVsPlayerGame()
	{
		initializePlayerPaddle(leftPaddle, PaddlePosition.Left);
		initializePlayerPaddle(rightPaddle, PaddlePosition.Right);
	}

	void initializeAIPaddle(GameObject paddle, PaddlePosition position)
	{
		paddle.AddComponent<AIPaddleController>().Initialize(puck, position, leftGoal);
	}

	void initializePlayerPaddle(GameObject paddle, PaddlePosition position)
	{
		paddle.AddComponent<PlayerPaddleController>().Initialize(puck, position, rightGoal);
	}
}
