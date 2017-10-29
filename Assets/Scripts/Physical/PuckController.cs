﻿using UnityEngine;
using System.Collections;

public class PuckController : PhysicalObjectController 
{
    [SerializeField]
    float perpendicularTolerance = 0.01f;
	TrailRenderer trail;

	public bool IsAlive
	{
		get;
		private set;
	}

	// Use this for initialization
	protected override void Start() 
	{
		base.Start();
		trail = GetComponent<TrailRenderer>();
		Init();
		Game game = StateController.Instance.CurrentGame;
		game.OnStart.Subscribe(
			delegate
			{
				gameObject.SetActive(true);
				SpawnPuck();
			}
		);
		game.OnEnd.Subscribe(
			delegate
			{
				TogglePuck(false);
				gameObject.SetActive(false);
			}
		);
	}

	void Init() 
	{
		Subscribe();
		SpawnPuck();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		Unsubscribe();
	}

	void Subscribe()
	{
		EventControler.OnNamedEvent += HandleNamedEvent;
	}

	void Unsubscribe()
	{
		EventControler.OnNamedEvent -= HandleNamedEvent;
	}

	void HandleNamedEvent(string eventName) 
	{
		if(eventName == EventList.GOAL && gameObject.activeInHierarchy)
		{
			SpawnPuck(Global.PUCK_RESPAWN_TIME);
		}
	}

	void SpawnPuck (float waitTime = Global.PUCK_RESPAWN_TIME) {
		StartCoroutine(TimedSpawnPuck(waitTime));
	}

	public float GetYPosition()
	{
		return transform.localPosition.y;
	}

	public float GetXPosition()
	{
		return transform.localPosition.x;
	}

	public void TogglePuck (bool active) {
		sprite.enabled = active;
		trail.enabled = active;
		if(active) 
		{
			rigibody.WakeUp();
		} 
		else 
		{
			rigibody.Sleep();
		}
		IsAlive = active;
	}

	IEnumerator TimedSpawnPuck (float waitTime = 0) {
		TogglePuck(false);
		yield return new WaitForSeconds(waitTime);
		TogglePuck(true);

		transform.position = Vector2Util.Origin;
		RandomStartingVelocity();
	}

	void FixedUpdate () {
		CapSpeed();
	}

	void CapSpeed () {
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

	void MaintainMomentum () {
		rigibody.velocity = Vector2Util.SpeedBoost(rigibody.velocity);
	}

	void RandomStartingVelocity () {
		rigibody.velocity = new Vector2 (
			Random.Range (-Global.MAX_PUCK_STARTING_SPEED, Global.MAX_PUCK_STARTING_SPEED),
			Random.Range (-Global.MAX_PUCK_STARTING_SPEED, Global.MAX_PUCK_STARTING_SPEED)
		);
	}

	protected override void OnCollisionEnter2D (Collision2D collision) 
	{
		base.OnCollisionEnter2D(collision);
		EventControler.Event(collision.gameObject.tag);
	}

	void OnCollisionExit2D (Collision2D collision) 
	{
		PreventPerpendicularPaths();
		MaintainMomentum();
	}

	// TODO: Add a more elegant scoring system
	int ScoreGoal (PaddlePosition scoringPlayer) {
		return CalculateMultipliers(scoringPlayer) * Global.BASE_GOAL_SCORE;
	}

	int CalculateMultipliers (PaddlePosition scoringPlayer) {
		return 1;
	}
}
