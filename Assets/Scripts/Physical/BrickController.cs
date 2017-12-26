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

	Animator animator;
	SpriteRenderer sRenderer;

	public void Spawn(Sprite sprite)
	{
		gameObject.SetActive(true);
		animator.enabled = false;
		sRenderer.sprite = sprite;
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
	}

	protected override void OnCollisionEnter2D(Collision2D collision) 
	{
		base.OnCollisionEnter2D(collision);
		if(collision.gameObject.tag == Global.PUCK) 
		{
			EventController.Event(Global.BRICK);
			Break();
		}
	}

	void Break()
	{
		EventController.Event(EventList.BRICK_BROKEN);
		animator.enabled = true;
	}
}
