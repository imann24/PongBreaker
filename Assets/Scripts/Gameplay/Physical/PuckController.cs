using UnityEngine;
using System.Collections;

public class PuckController : PhysicalObjectController 
{
	const string PUCK_HIT_EVENT = "PuckHit";

	public bool IsAlive
	{
		get;
		private set;
	}

    [SerializeField]
    float perpendicularTolerance = 0.01f;

	GameplayController gameplay;
	TrailRenderer trail;

	bool hasScored = false;
	bool inGameSpawn = false;

	protected override void Awake()
	{
		base.Awake();
		trail = GetComponent<TrailRenderer>();
	}

	// Use this for initialization
	protected override void Start() 
	{
		base.Start();
		Init();
		Game game = StateController.Instance.CurrentGame;
		game.OnStart.Subscribe(
			delegate
			{
				gameObject.SetActive(true);
				SpawnPuckFromStart();
			}
		);
		game.OnEnd.Subscribe(
			delegate
			{
				TogglePuck(false);
				gameObject.SetActive(false);
			}
		);
		gameplay = GameplayController.Instance;
		gameplay.RegisterPuck(this);
	}
		
	protected override void HandleNamedEvent(string eventName)
	{
		base.HandleNamedEvent(eventName);
		if(eventName == Global.GOAL && !hasScored)
		{
			// Deactivate all other pucks when there is a goal
			TogglePuck(active:false);
			gameObject.SetActive(false);
			gameplay.DeactivatePuck(this);
		}
	}

	void Init() 
	{
		if(inGameSpawn)
		{
			inGameSpawn = false;
		}
		else
		{
			SpawnPuckFromStart();
		}
	}

	public void SpawnPuck(Vector2 position)
	{
		transform.position = position;
		TogglePuck(active:true);
		RandomStartingVelocity();
		hasScored = false;
		inGameSpawn = true;
	}

	void SpawnPuckFromStart(float waitTime = Global.PUCK_RESPAWN_TIME) 
	{
		StartCoroutine(TimedSpawnPuck(waitTime));
	}

	public void TogglePuck(bool active) 
	{
		sprite.enabled = active;
		trail.enabled = active;
		rigibody.isKinematic = !active;
		IsAlive = active;
	}

	IEnumerator TimedSpawnPuck(float waitTime = 0) 
	{
		TogglePuck(false);
		yield return new WaitForSeconds(waitTime);
		transform.position = Vector2Util.Origin;
		TogglePuck(true);
		RandomStartingVelocity();
		hasScored = false;
	}

	void FixedUpdate() 
	{
		CapSpeed();
	}

	void CapSpeed()
	{
		rigibody.velocity = Vector2Util.Clamp(rigibody.velocity, Global.MAX_PUCK_SPEED, -Global.MAX_PUCK_SPEED);
	}

    void PreventPerpendicularPaths()
    {
        if (isOnPerpendicularPath())
        {
            rigibody.velocity = Vector2Util.MakeDiagonal(rigibody.velocity);
        }
    }

    bool isOnPerpendicularPath()
    {
        return withinPerpendicularTolerance(rigibody.velocity.x) || withinPerpendicularTolerance(rigibody.velocity.y);
    }

    bool withinPerpendicularTolerance(float value)
    {
        return Mathf.Abs(value) < perpendicularTolerance;
    }

	void MaintainMomentum() 
	{
		rigibody.velocity = Vector2Util.SpeedBoost(rigibody.velocity);
	}

	void RandomStartingVelocity() 
	{
		rigibody.velocity = new Vector2 (
			Random.Range (-Global.MAX_PUCK_STARTING_SPEED, Global.MAX_PUCK_STARTING_SPEED),
			Random.Range (-Global.MAX_PUCK_STARTING_SPEED, Global.MAX_PUCK_STARTING_SPEED)
		);
	}

	protected override void OnCollisionEnter2D(Collision2D collision) 
	{
		base.OnCollisionEnter2D(collision);
		string collisionTag = collision.gameObject.tag;
		PaddleController paddle;
		if(PlayerUtil.IsGoalTag(collisionTag))
		{
			if(!hasScored)
			{
				hasScored = true;
				if(gameObject.activeInHierarchy)
				{
					SpawnPuckFromStart(Global.PUCK_RESPAWN_TIME);
				}
				EventController.Event(collisionTag);
			}
		}
		else if(paddle = collision.collider.GetComponent<PaddleController>())
		{
			gameplay.RegisterPlayerHit(paddle);
			EventController.Event(PUCK_HIT_EVENT);
		}
		else
		{
			EventController.Event(collisionTag);
		}
	}

	void OnCollisionExit2D (Collision2D collision) 
	{
		PreventPerpendicularPaths();
		MaintainMomentum();
	}
}
