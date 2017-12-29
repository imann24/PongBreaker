using UnityEngine;
using System.Collections;

public class BrickController : PhysicalObjectController 
{
	public bool IsDestroyed
	{
		get
		{
			return gameObject.activeSelf;
		}
	}

	public bool CanSpawnPowerUps
	{
		get
		{
			return canSpawnPowerUps;
		}
	}

	GameplayController game;
	Animator animator;
	SpriteRenderer sRenderer;
	[SerializeField]
	bool canSpawnPowerUps = true;
	bool isBreaking = false;

	public void Spawn(Sprite sprite)
	{
		gameObject.SetActive(true);
		animator.enabled = false;
		sRenderer.sprite = sprite;
		isBreaking = false;
	}

	protected override void Start()
	{
		base.Start();
		SetReferences();
	}

	void SetReferences()
	{
		animator = GetComponent<Animator>();
		sRenderer = GetComponent<SpriteRenderer>();
		game = GameplayController.Instance;
	}

	protected override void OnCollisionEnter2D(Collision2D collision) 
	{
		base.OnCollisionEnter2D(collision);
		if(!isBreaking && collision.gameObject.tag == Global.PUCK) 
		{
			EventController.Event(Global.BRICK);
			Break();
			game.HandleBrickDestroyed(this);
			isBreaking = true;
		}
	}

	void Break()
	{
		EventController.Event(EventList.BRICK_BROKEN);
		animator.enabled = true;
	}
}
