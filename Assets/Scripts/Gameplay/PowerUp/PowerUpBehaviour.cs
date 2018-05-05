/*
 * Author: Isaiah Mann
 * Description: Controls behaviour of physical powerup object
 */

using UnityEngine;

public abstract class PowerUpBehaviour : PhysicalObjectController
{
	public float GetSpawnWeight
	{
		get
		{
			return spawnWeight;
		}
	}

	protected GameplayController gameplay;

	[Header("Time in seconds which the power up's effects last")]
	[SerializeField]
	protected float duration = 1.0f;

	[Header("How likely power up is to spawn")]
	[SerializeField]
	float spawnWeight = 1.0f;

	[Header("How fast the power up moves towards the player")]
	[SerializeField]
	float travelSpeed = 2.0f;

	PowerUpController controller;

	public virtual void Use(PaddleController player)
	{
		controller.AddToSpawnPool(this);
	}

	protected override void OnEnable()
	{
        base.OnEnable();
        Awake();
        Start();
	}

	protected override void Awake()
	{
		base.Awake();
		rigibody = GetComponent<Rigidbody2D>();
	}

	protected override void Start()
	{
		base.Start();
		gameplay = GameplayController.Instance;
		controller = PowerUpController.Instance;
	}

	public void Place(BrickController brick)
	{
		transform.position = brick.transform.position;
	}

	public void Move(PaddlePosition towardsPlayer)
	{
		switch(towardsPlayer)
		{
			case PaddlePosition.Left:
				rigibody.velocity = Vector2.left * travelSpeed;
				break;
			case PaddlePosition.Right:
				rigibody.velocity = Vector2.right * travelSpeed;
				break;
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		PaddleController player;
		if((player = collider.GetComponent<PaddleController>()) && 
			!collider.isTrigger) // There is a larger trigger collider on the paddles used for touch/drag
		{
			Use(player);
		}
		else if(collider.tag.Contains(Global.GOAL))
		{
			controller.AddToSpawnPool(this);
		}
	}
}
